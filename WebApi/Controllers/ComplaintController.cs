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

    public ComplaintController(IComplaintService complaintService)
    {
        _complaintService = complaintService;
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
    public async Task<IActionResult> GetComplaints(int page, int pageSize, long? userId = null)
    {
        var result = await _complaintService.GetComplaints(userId);
        result = result.AsEnumerable().Skip((page - 1) * pageSize).Take(pageSize).ToArray();

        return Ok(result);
    }
}