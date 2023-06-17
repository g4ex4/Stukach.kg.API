using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Common;
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

    public async Task<Response> Login(LoginRegisterData loginRegister)
    {
        var user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x =>
            x.PhoneNumber == loginRegister.PhoneNumber && x.Password == loginRegister.Password);
        return user is not null
            ? new Response(200, "Успешная авторизация", true)
            : throw new DException("Неправильный номер или пароль");
    }

    public async Task<AuthResponse> Register(LoginRegisterData loginRegister)
    {
        var result = await _unitOfWork.GetRepository<User>()
            .FirstOrDefaultAsync(x => x.PhoneNumber == loginRegister.PhoneNumber);
        if (result is not null)
        {
            throw new DException("Такой пользователь существует!");
        }
        var newUser = new User()
        {
            PhoneNumber = loginRegister.PhoneNumber,
            Password = loginRegister.Password
        };

        await _unitOfWork.GetRepository<User>().AddAsync(newUser);
        await _unitOfWork.SaveChanges();

        return new AuthResponse(200, "Успешная регистрация", true, newUser.Id);
    }
    
    
}