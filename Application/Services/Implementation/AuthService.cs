using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Dto;
using Domain.Models;

namespace Application.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Login(LoginRegisterData loginRegister)
    {
        var user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x =>
            x.PhoneNumber == loginRegister.PhoneNumber && x.Password == loginRegister.Password);
        return user is not null ? new AuthResponse("Успешная авторизация", user.Id) : new AuthResponse("Неудачно", 0);
    }

    public async Task<AuthResponse> Register(LoginRegisterData loginRegister)
    {
        var newUser = new User()
        {
            PhoneNumber = loginRegister.PhoneNumber,
            Password = loginRegister.Password
        };

        await _unitOfWork.GetRepository<User>().AddAsync(newUser);
        await _unitOfWork.SaveChanges();

        return new AuthResponse("Успешная регистрация", newUser.Id);
    }
}