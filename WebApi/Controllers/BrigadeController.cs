using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using Domain.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[EnableCors("CorsPolicy")]
[Route("[controller]")]
public class BrigadeController : Controller
{
    private readonly IBrigadeService _brigadeService;
    private readonly OpenCageApiClient _openCageApiClient;

    public BrigadeController(IBrigadeService brigadeService, OpenCageApiClient openCageApiClient)
    {
        _brigadeService = brigadeService;
        _openCageApiClient = openCageApiClient;
    }

    [HttpPost("Create-Brigade")]
    public async Task<IActionResult> CreateBrigade([FromBody] BrigadeDTO dto)
    {
        var result = _brigadeService.CreateBrigade(dto);
        return Ok(result);
    }


    [HttpPut("Set-ComplaintOn-Brigade")]
    public async Task<IActionResult> SetComplaintOnBridage(long complaintId, long brigadeId)
    {
        var result = _brigadeService.SetComplaintOnBridage(complaintId, brigadeId);
        return Ok(result);
    }

    [HttpGet("Get-Bridage's-ComplaintsByBrigadeId")]
    public async Task<IActionResult> GetComplaintsByBrigadeId(long brigadeId)
    {
        var result = _brigadeService.GetComplaintsByBrigadeId(brigadeId);
        return Ok(result);
    }
    
}