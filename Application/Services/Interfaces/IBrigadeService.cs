using Domain.Common;
using Domain.Dto;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IBrigadeService : IService
{
    Task<BrigadeResponse> CreateBrigade(BrigadeDTO dto);
}