using Domain.Dto;
using Domain.Enums;

namespace Application.Services.Interfaces;

public interface IComplaintService : IService
{
    Task AddComplaint(AddComplaintData complaint);
    Task PutStatusComplaint(long userId, long complaintId, ComplaintImportance importance);
    Task<ComplaintData[]> GetComplaints(long? userId);
}