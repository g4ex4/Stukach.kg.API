using Application.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("get-regions")]
    public async Task<IActionResult> GetRegions()
    {
        var result = await _addressService.GetRegions();

        return Ok(result);
    }

    [HttpGet("get-district")]
    public async Task<IActionResult> GetDistrictByRegion(long regionId)
    {
        var result = await _addressService.GetDistricts(regionId);

        return Ok(result);
    }

    [HttpGet("get-cities")]
    public async Task<IActionResult> GetCitiesByDistrict(long districtId)
    {
        var result = await _addressService.GetCities(districtId);

        return Ok(result);
    }
}