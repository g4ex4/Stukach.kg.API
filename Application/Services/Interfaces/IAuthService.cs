using Domain.Dto;

namespace Application.Services.Interfaces;

public interface IAuthService : IService
{
    Task<AuthResponse> Login(LoginRegisterData loginRegister);
    Task<AuthResponse> Register(LoginRegisterData loginRegister);
}