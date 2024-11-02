﻿using Application.Interfaces;
using Entities.AuthDto;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController : Controller
{
    private readonly IAuthService _authService;

    public TokenController(IAuthService authService) => _authService = authService;

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var tokenDtoToReturn = await _authService.RefreshToken(tokenDto);
        return Ok(tokenDtoToReturn);
    }
}
