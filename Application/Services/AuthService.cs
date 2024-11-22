using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Contracts;
using Application.DataTransferObjects;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.AuthDto;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationManager _authManager;
    private readonly ILoggerManager _logger;
    private readonly IConfiguration _configuration;

    public AuthService(IMapper mapper, UserManager<User> userManager,
        IAuthenticationManager authManager, ILoggerManager logger,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _authManager = authManager;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);

        if (result.Succeeded)
        {
            var userRoleAsString = userForRegistration.Role.ToString();
            await _userManager.AddToRolesAsync(user, new List<string> { userRoleAsString });
        }
        else
        {
            _logger.LogError("Registration failed");
        }

        return result;
    }

    public async Task<(string AccessToken, string RefreshToken)> AuthenticateUserAsync(UserForAuthenticationDto userForLogin)
    {
        var user = await _userManager.FindByNameAsync(userForLogin.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, userForLogin.Password))
        {
            _logger.LogWarn("Invalid login attempt.");
            return (null, null);
        }
        await _authManager.ValidateUser(userForLogin);
        var tokenDto = await _authManager.CreateToken(user, populateExp: true);
        return (tokenDto.AccessToken, tokenDto.RefreshToken);
    }
    
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings.GetSection("validIssuer").Value;
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
            ValidateLifetime = true,
            ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
            ValidAudience = jwtSettings.GetSection("validAudience").Value,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }
        return principal;
    }

    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);
        if (user is null || user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpireTime <= DateTime.UtcNow)
        {
            return null;
        }
        return await _authManager.CreateToken(user, populateExp: false);
    }
}