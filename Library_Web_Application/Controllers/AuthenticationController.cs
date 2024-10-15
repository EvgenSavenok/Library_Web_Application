using AutoMapper;
using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationManager _authManager;
    private ILoggerManager _logger;
    public AuthenticationController (IMapper mapper, UserManager<User> userManager, IAuthenticationManager authManager,
        ILoggerManager logger)
    {
        _mapper = mapper;
        _userManager = userManager;
        _authManager = authManager;
        _logger = logger;
    }

    [HttpGet("registerPage")]
    public IActionResult RegisterPage()
    {
        return View("~/Views/Auth/RegisterPage.cshtml");
    }
    
    [HttpGet("loginPage")]
    public IActionResult LoginPage()
    {
        return View("~/Views/Auth/LoginPage.cshtml");
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }
        var rolesAsStrings = Enum.GetValues(typeof(UserForRegistrationDto.UserRole))
            .Cast<UserForRegistrationDto.UserRole>()            
            .Select(role => role.ToString()) 
            .ToList(); 
        await _userManager.AddToRolesAsync(user, rolesAsStrings);
        return StatusCode(201);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        if (!await _authManager.ValidateUser(user))
        {
            return Unauthorized();
        }
        return Ok(new { Token = await _authManager.CreateToken() });
    }
}
