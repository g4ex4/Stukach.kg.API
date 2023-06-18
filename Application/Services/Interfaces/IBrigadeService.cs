using Domain.Common;
using Domain.Dto;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IBrigadeService : IService
{
    Task<BrigadeResponse> CreateBrigade(BrigadeDTO dto);
    Task<Response> SetComplaintOnBridage(long complaintId, long brigadeId);
    Task<Complaint[]> GetComplaintsByBrigadeId(long brigadeId);
}