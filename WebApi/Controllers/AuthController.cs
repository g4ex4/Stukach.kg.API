using Application.Services.Interfaces;
using Domain.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRegisterData loginRegister)
    {
        var response = await _authService.Login(loginRegister);

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRegisterData loginRegister)
    {
        var response = await _authService.Register(loginRegister);

        return Ok(response);
    }
}