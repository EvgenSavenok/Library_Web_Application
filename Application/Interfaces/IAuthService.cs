using Application.DataTransferObjects;
using Entities;
using Entities.AuthDto;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration);
    Task<(string AccessToken, string RefreshToken)> AuthenticateUserAsync(UserForAuthenticationDto user);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}