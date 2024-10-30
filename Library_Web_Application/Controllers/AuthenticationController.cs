﻿using AutoMapper;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.DataTransferObjects;
using Application.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Authentication;

namespace Library_Web_Application.Controllers;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly IAuthService _authService;

    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
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
        if (userForRegistration == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid registration request");
        }

        var result = await _authService.RegisterUserAsync(userForRegistration);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return StatusCode(201);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto userForLogin)
    {
        if (userForLogin == null || !ModelState.IsValid)
        {
            return BadRequest("Invalid login request");
        }

        var token = await _authService.AuthenticateUserAsync(userForLogin);
        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}
