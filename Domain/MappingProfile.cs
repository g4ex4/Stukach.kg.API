using AutoMapper;
using Domain.Dto;
using Domain.Models;

namespace Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Complaint, ComplaintData>()
            .ForMember(x => x.FullAddress, 
                opt => opt.MapFrom(x => x.Coordinate.GetFullAddress()))
            .ForMember(x => x.Author,
                opt => opt.MapFrom(x =>new UserData(){PhoneNumber = x.Author.PhoneNumber}))
            .ForMember(x => x.Coordinate,
                opt => opt.MapFrom(x => new CoordinateData(){Longitude = x.Coordinate.Longitude, Latitude = x.Coordinate.Latitude, ComplaintId = x.Id}))
            .ReverseMap();
    }
}