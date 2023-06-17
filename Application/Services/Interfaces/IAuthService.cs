using Domain.Dto;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IAuthService : IService
{
    Task<AuthResponse> Login(LoginRegisterData loginRegister);
    Task<AuthResponse> Register(LoginRegisterData loginRegister);
    Task<UserData[]> GetAllUsers();
}