using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dal.interfaces;
using Domain.Common;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Login(LoginRegisterData loginRegister)
    {
        var user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x =>
            x.PhoneNumber == loginRegister.PhoneNumber && x.Password == loginRegister.Password);
        return user is not null
            ? new AuthResponse(200, "Успешная авторизация", true, user.Id)
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
    

    public async Task<UserData[]> GetAllUsers()
    {
        var result = await _unitOfWork.GetRepository<User>()
            .GetAll()
            .ProjectTo<UserData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();
        return result;
    }

    
}