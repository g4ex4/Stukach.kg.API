using Domain.Common;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IComplaintService : IService
{
    Task AddComplaint(AddComplaintData complaint);
    Task<Response> PutStatusComplaint(long userId, long complaintId, ComplaintImportance importance);
    Task<ComplaintData[]> GetComplaints(long? userId);
    Task<Response> ChangeComplaintStatus(long complaintId, ComplaintStatus status);
    Task<ComplaintData[]> GetComplaintsByStatus(ComplaintStatus status);
    Task<User[]> GetAllUsers();
}