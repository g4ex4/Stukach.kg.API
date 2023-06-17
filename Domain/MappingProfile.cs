using AutoMapper;
using Domain.Dto;
using Domain.Models;

namespace Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ComplaintData, Complaint>()
            .ReverseMap();
    }
}