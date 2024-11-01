using Application.DataTransferObjects;
using Application.Interfaces;
using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationManager _authManager;
    private readonly ILoggerManager _logger;

    public AuthService(IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager, ILoggerManager logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _authManager = authManager;
        _logger = logger;
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

    public async Task<string> AuthenticateUserAsync(UserForAuthenticationDto userForLogin)
    {
        var user = await _userManager.FindByNameAsync(userForLogin.UserName);
        if (user == null || !await _userManager.CheckPasswordAsync(user, userForLogin.Password))
        {
            _logger.LogWarn("Invalid login attempt.");
            return null;
        }
        await _authManager.ValidateUser(userForLogin);
        return await _authManager.CreateToken();
    }
}