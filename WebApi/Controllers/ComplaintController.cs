﻿using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using Domain.Common;
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

    public ComplaintController(IComplaintService complaintService, OpenCageApiClient openCageApiClient)
    {
        _complaintService = complaintService;
    }

    [HttpPost("add-complaint")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddComplaint([FromBody] AddComplaintData complaint)
    {
        var result = await _complaintService.AddComplaint(complaint);

        return Ok(result);
    }
    
    [HttpPut("put-complaint-importance")]
    public async Task<IActionResult> PutComplaintImportance(long userId, long complaintId, ComplaintImportance importance)
    {
        Response result;
        try
        {
            result = await _complaintService.PutComplaintImportance(userId, complaintId, importance);
        }
        catch (NotFoundException e)
        {
            return BadRequest(new Response(400, e.Message, false));
        }

        return Ok(result);
    }

    [HttpGet("get-complaints")]
    [ProducesResponseType(typeof(ComplaintData[]), 200)]
    public async Task<IActionResult> GetComplaints(long? userId = null)
    {
        var result = await _complaintService.GetComplaints(userId);

        return Ok(result);
    }

    [HttpPut("put-complaint-status")]
    public async Task<IActionResult> PutComplaintStatus(long complaintId, ComplaintStatus status)
    {
        var result = await _complaintService.PutComplaintStatus(complaintId, status);

        return Ok(result);
    }

    [HttpGet("get-complaints-by-status")]
    [ProducesResponseType(typeof(ComplaintData[]), 200)]
    public async Task<IActionResult> GetComplaintsByStatus(ComplaintStatus status)
    {
        var result = await _complaintService.GetComplaintsByStatus(status);
        return Ok(result);
    }


    [HttpGet("get-complaints-by-id")]
    [ProducesResponseType(typeof(ComplaintData), 200)]
    public async Task<IActionResult> GetComplaintsById(long complaintId)
    {
        var result = await _complaintService.GetComplaintsById(complaintId);
        return Ok(result);
    }

    [HttpGet("get-complaints-by-address")]
    public async Task<IActionResult> GetComplaintsByAddress(long? regionId, long? districtId, long? cityId)
    {
        var result = await _complaintService.GetComplaintByAddress(regionId, districtId, cityId);
        
        return Ok(result);
    }
    
    [HttpGet("get-complaints-by-user-id")]
    [ProducesResponseType(typeof(Complaint[]), 200)]
    public async Task<IActionResult> GetComplaintsByUserId(long userId)
    {
        Complaint result;
        try
        {
            result = await _complaintService.GetComplaintsByUserId(userId);
        }
        catch (NotFoundException e)
        {
            return BadRequest(new Response(400, e.Message, false));
        }
        return Ok(result);
    }
}