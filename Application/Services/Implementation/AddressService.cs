using Application.Integrations.Geocoding.Models;
using Application.Services.Interfaces;
using Dal.interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;

    public AddressService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Tuple<Region, District, City>> FillAddress(GeoCodeResponse geoCodeResponse)
    {
        var response = geoCodeResponse.Results[0].Components;

        var cityName = response.City ?? response.Hamlet ?? response.Village ?? response.Town;

        var city = await _unitOfWork.GetRepository<City>()
            .FirstOrDefaultAsync(x => x.Name == cityName) ?? new City()
        {
            Name = cityName
        };

        var district = await _unitOfWork.GetRepository<District>()
            .Include(x => x.Cities)
            .FirstOrDefaultAsync(x => x.Name == response.County);

        if (district is not null)
        {
            district.Cities.Add(city);
        }
        if (district is null && response.County is not null)
        {
            district = new District
            {
                Name = response.County,
                Cities = new List<City>()
            };
            district.Cities.Add(city);
        }

        var region = await _unitOfWork.GetRepository<Region>()
            .Include(x => x.Cities)
            .Include(x => x.Districts)
            .FirstOrDefaultAsync(x => x.Name == response.State);

        if (region is not null)
        {
            if(district is not null) region.Districts.Add(district);
            else region.Cities.Add(city);
        }

        if (region is not null || response.State is null)
            return new Tuple<Region, District, City>(region, district, city);
        region = new Region()
        {
            Name = response.State
        };
        region.Districts = new List<District>();
        region.Cities = new List<City>();
        if(district is not null) region.Districts.Add(district);
        region.Cities.Add(city);

        return new Tuple<Region, District, City>(region, district, city);
    }

    public async Task<Region[]> GetRegions()
    {
        var regions = await _unitOfWork.GetRepository<Region>()
            .GetAll()
            .ToArrayAsync();

        return regions;
    }

    public async Task<District[]> GetDistricts(long regionId)
    {
        var districts = await _unitOfWork.GetRepository<District>()
            .Where(x => x.RegionId == regionId)
            .ToArrayAsync();

        return districts;
    }

    public async Task<City[]> GetCities(long districtId)
    {
        var cities = await _unitOfWork.GetRepository<City>()
            .Where(x => x.DistrictId == districtId)
            .ToArrayAsync();

        return cities;
    }
}