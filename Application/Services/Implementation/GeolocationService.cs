using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Dto;
using Domain.Models;

namespace Application.Services.Implementation;

public class GeolocationService : IGeolocationService
{
    private readonly IUnitOfWork _unitOfWork;

    public GeolocationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SaveCoordinate(CoordinateData coordinate)
    {
        
    }
}