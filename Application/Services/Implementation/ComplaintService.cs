using Application.Services.Interfaces;
using AutoMapper;
using Dal.interfaces;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation;

public class ComplaintService : IComplaintService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ComplaintService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddComplaint(AddComplaintData complaint)
    {
        var newComplaint = new Complaint();
        newComplaint.Date = complaint.Date;
        newComplaint.Name = complaint.Name;
        newComplaint.ImageUrl = complaint.Image.Name;
        newComplaint.Status = ComplaintStatus.Created;
        await _unitOfWork.GetRepository<Complaint>().AddAsync(newComplaint);
        await _unitOfWork.SaveChanges();
    }

    public async Task PutStatusComplaint(long userId, long complaintId, ComplaintImportance importance)
    {
        var complaint = await _unitOfWork
            .GetRepository<Complaint>()
            .FirstOrDefaultAsync(x => x.Id == complaintId);

        var userComplaint = await _unitOfWork
                                .GetRepository<UserComplaint>()
                                .FirstOrDefaultAsync(x => x.UserId == userId && x.ComplaintId == complaintId)
                            ?? new UserComplaint
                            {
                                UserId = userId,
                                ComplaintId = complaintId
                            };

        userComplaint.Importance = importance;

        switch (importance)
        {
            case ComplaintImportance.Like:
                complaint.CountLike += 1;
                break;
            case ComplaintImportance.Dislike:
                complaint.CountDislike += 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(importance), importance, null);
        }
    }

    public async Task<UserComplaintData> GetComplaints(long userId)
    {
        var complaints = await _unitOfWork.GetRepository<Complaint>()
            .GetAll()
            .ToListAsync();

        var userComplaint = await _unitOfWork.GetRepository<UserComplaint>()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        // var userComplaints = from 
        return new UserComplaintData();
    }
}