using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Common;
using Domain.Dto;
using Domain.Models;

namespace Application.Services.Implementation;

public class BrigadeService : IBrigadeService
{
    private IUnitOfWork _unitOfWork;

    public BrigadeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BrigadeResponse> CreateBrigade(BrigadeDTO dto)
    {
        var newBrigade = new Brigade()
        {
            BrigadeNumber = dto.BrigadeNumber
        };
        await _unitOfWork.GetRepository<Brigade>().AddAsync(newBrigade);
        await _unitOfWork.SaveChanges();
        return new BrigadeResponse(200, "Бригада успешно создана", true, newBrigade.Id);
    }
}