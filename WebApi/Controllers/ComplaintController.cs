using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("[controller]")]
public class ComplaintController : Controller
{
    private readonly IComplaintService _complaintService;
    private readonly OpenCageApiClient _openCageApiClient;

    public ComplaintController(IComplaintService complaintService, OpenCageApiClient openCageApiClient)
    {
        _complaintService = complaintService;
        _openCageApiClient = openCageApiClient;
    }

    [HttpPost("add-complaint")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddComplaint([FromBody] AddComplaintData complaint)
    {
        var result = await _complaintService.AddComplaint(complaint);

        return Ok(result);
    }
    
    [HttpPut("put-complaint-status")]
    public async Task<IActionResult> PutComplaintStatus(long userId, long complaintId, ComplaintImportance importance)
    {
        var result = await _complaintService.PutStatusComplaint(userId, complaintId, importance);

        return Ok(result);
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
        var response = _openCageApiClient.Geocode($"{coordinateData.Latitude}+{coordinateData.Longitude}");
        return Ok(response);
    }

    [HttpPut("put-complaint-status-for-admin")]
    public async Task<IActionResult> ChangeComplaintStatus(long complaintId, ComplaintStatus status)
    {
        var result = await _complaintService.ChangeComplaintStatus(complaintId, status);

        return Ok(result);
    }

    [HttpGet("Get-complaints-byComplaintStatus")]
    [ProducesResponseType(typeof(Complaint[]), 200)]
    public async Task<IActionResult> GetComplaintsByStatus(ComplaintStatus status)
    {
        var result = await _complaintService.GetComplaintsByStatus(status);
        return Ok(result);
    }

    [HttpGet("Get-complaints-byId")]
    [ProducesResponseType(typeof(Complaint[]), 200)]
    public async Task<IActionResult> GetComplaintsById(long complaintId)
    {
        var result = await _complaintService.GetComplaintsById(complaintId);
        return Ok(result);
    }
}