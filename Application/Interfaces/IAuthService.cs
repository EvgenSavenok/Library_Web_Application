using Application.DataTransferObjects;
using Entities;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(UserForRegistrationDto userForRegistration);
    Task<string> AuthenticateUserAsync(UserForAuthenticationDto user);
}