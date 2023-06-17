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

    [HttpPost("create-brigade")]
    public async Task<IActionResult> CreateBrigade([FromBody] BrigadeDTO dto)
    {
        var result = _brigadeService.CreateBrigade(dto);
        return Ok(result);
    }


    [HttpPut("set-complaintOn-brigade")]
    public async Task<IActionResult> SetComplaintOnBridage(long complaintId, long brigadeId)
    {
        var result = _brigadeService.SetComplaintOnBridage(complaintId, brigadeId);
        return Ok(result);
    }

    [HttpGet("get-bridage's-complaints-by-brigadeId")]
    public async Task<IActionResult> GetComplaintsByBrigadeId(long brigadeId)
    {
        var result = _brigadeService.GetComplaintsByBrigadeId(brigadeId);
        return Ok(result);
    }
    
    [HttpDelete("Delete-BrigadeByNumber")]
    public async Task<IActionResult> DeleteBridageByNumber(int bridageNum)
    {
        var result = _brigadeService.DeleteBrigadeByNumber(bridageNum);
        return Ok(result);
    }


}