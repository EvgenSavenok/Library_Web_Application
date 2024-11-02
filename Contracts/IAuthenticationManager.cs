using Entities.AuthDto;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts;

public interface IAuthenticationManager
{
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<TokenDto> CreateToken(User user, bool populateExp);
}
