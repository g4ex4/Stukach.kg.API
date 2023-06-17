using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using Domain.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("[controller]")]
public class ComplaintController : Controller
{
    private readonly IComplaintService _complaintService;
    private readonly Geocoding _geocoding;

    public ComplaintController(IComplaintService complaintService, Geocoding geocoding)
    {
        _complaintService = complaintService;
        _geocoding = geocoding;
    }

    [HttpPost("add-complaint")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddComplaint([FromBody] AddComplaintData complaint)
    {
        await _complaintService.AddComplaint(complaint);

        return Ok();
    }
    
    [HttpPut("put-complaint-status")]
    public async Task<IActionResult> PutComplaintStatus(long userId, long complaintId, ComplaintImportance importance)
    {
        await _complaintService.PutStatusComplaint(userId, complaintId, importance);

        return Ok();
    }

    [HttpGet("get-complaints")]
    [ProducesResponseType(typeof(ComplaintData[]), 200)]
    public async Task<IActionResult> GetComplaints(long? userId = null)
    {
        var result = await _complaintService.GetComplaints(userId);

        return Ok(result);
    }

    [HttpGet("check")]
    public async Task<IActionResult> CheckCoordinate([FromQuery] CoordinateData coordinateData)
    {
        var response = _geocoding.Geocode(coordinateData);
        return Ok(response);
    }
}