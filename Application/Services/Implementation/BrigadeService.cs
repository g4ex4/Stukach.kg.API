using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Common;
using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Response> SetComplaintOnBridage(long complaintId, long brigadeId)
    {
        var complaint = await _unitOfWork.GetRepository<Complaint>()
            .FirstOrDefaultAsync(x => x.Id == complaintId);
        if (complaint is null)
            throw new NotFoundException(nameof(Complaint), complaintId);
        var brigade = await _unitOfWork.GetRepository<Brigade>()
            .FirstOrDefaultAsync(x => x.Id == brigadeId);
        if (brigade is null)
            throw new NotFoundException(nameof(Brigade), brigadeId);
        complaint.Brigade.Id = brigadeId;
        brigade.Complaints.Add(complaint);
        _unitOfWork.GetRepository<Brigade>().Update(brigade);
        _unitOfWork.GetRepository<Complaint>().Update(complaint);
        await _unitOfWork.SaveChanges();
        return new Response
            (200, $"Бригаде {brigade.BrigadeNumber} установлена задача {complaint.Name}", true);
    }

    public async Task<Complaint[]> GetComplaintsByBrigadeId(long brigadeId)
    {
        var brigade = _unitOfWork.GetRepository<Brigade>()
            .FirstOrDefaultAsync(x => x.Id == brigadeId);
        if (brigade is null)
            throw new NotFoundException(nameof(brigade), brigadeId);
        var complaints = _unitOfWork.GetRepository<Complaint>()
            .Where(x => x.BrigadeId == brigadeId);
        return await complaints.ToArrayAsync();
    }
}