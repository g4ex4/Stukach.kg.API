using Domain.Common;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IComplaintService : IService
{
    Task<Response> AddComplaint(AddComplaintData complaint);
    Task<Response> PutComplaintImportance(long userId, long complaintId, ComplaintImportance importance);
    Task<ComplaintData[]> GetComplaints(long? userId);
    Task<Response> PutComplaintStatus(long complaintId, ComplaintStatus status);
    Task<ComplaintData[]> GetComplaintsByStatus(ComplaintStatus status);
    Task<ComplaintData> GetComplaintsById(long complainId);
    Task<ComplaintData[]> GetComplaintByAddress(long? regionId, long? districtId, long? cityId);
    Task<Complaint> GetComplaintsByUserId(long userId);
}