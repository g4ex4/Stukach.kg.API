using Application.Services.Interfaces;
using Domain.Common;
using Domain.Dto;
using Domain.Models;
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
        Response response;
        try
        {
            response = await _authService.Login(loginRegister);
        }
        catch (DException e)
        {
            return BadRequest(new Response(400, e.Message, false));
        }
        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRegisterData loginRegister)
    {
        Response response;
        try
        {
            response = await _authService.Register(loginRegister);
        }
        catch (DException e)
        {
            return BadRequest(new Response(400, e.Message, false));
        }
        return Ok(response);
    }
    
    
    [HttpGet("get-all-users")]
    [ProducesResponseType(typeof(User[]), 200)]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _authService.GetAllUsers();
        return Ok(result);
    }
}