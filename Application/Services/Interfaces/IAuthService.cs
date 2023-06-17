using Domain.Dto;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IAuthService : IService
{
    Task<Response> Login(LoginRegisterData loginRegister);
    Task<AuthResponse> Register(LoginRegisterData loginRegister);
}