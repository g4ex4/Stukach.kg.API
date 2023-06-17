using Application.Integrations.Geocoding.Models;
using Domain.Models;

namespace Application.Services.Interfaces;

public interface IAddressService : IService
{
    Task<Tuple<Region, District, City>> FillAddress(GeoCodeResponse geoCodeResponse);
    Task<Region[]> GetRegions();
    Task<District[]> GetDistricts(long regionId);
    Task<City[]> GetCities(long districtId);
}