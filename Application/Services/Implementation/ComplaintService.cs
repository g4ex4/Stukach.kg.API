using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using AutoMapper;
using Dal.interfaces;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementation;

public class ComplaintService : IComplaintService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly OpenCageApiClient _openCageApiClient;
    private readonly IAddressService _addressService;

    public ComplaintService(IUnitOfWork unitOfWork, 
        IMapper mapper, 
        OpenCageApiClient openCageApiClient,
        IAddressService addressService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _openCageApiClient = openCageApiClient;
        _addressService = addressService;
    }

    public async Task AddComplaint(AddComplaintData complaint)
    {
        var newComplaint = new Complaint();
        newComplaint.Date = complaint.Date;
        newComplaint.Name = complaint.Name;
        newComplaint.AuthorId = complaint.UserId;
        newComplaint.Description = complaint.Description;
        // newComplaint.ImageUrl = complaint.Image.Name;
        newComplaint.Status = ComplaintStatus.Created;
        // var uploadsFolder = Path.Combine("wwwroot", "uploads");
        // var uniqueFileName = complaint.Image.FileName;
        // var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //
        // await using (var stream = new FileStream(filePath, FileMode.Create))
        // {
        //     await complaint.Image.CopyToAsync(stream);
        // }
        var geoResponse = await _openCageApiClient
            .Geocode($"{complaint.Coordinate.Latitude}+{complaint.Coordinate.Longitude}");
        var address = await _addressService.FillAddress(geoResponse);
        newComplaint.Coordinate = new Coordinate
        {
            Latitude = complaint.Coordinate.Latitude,
            Longitude = complaint.Coordinate.Longitude,
            Region = address.Item1,
            District = address.Item2,
            City = address.Item3
        };
        await _unitOfWork.GetRepository<Complaint>().AddAsync(newComplaint);
        await _unitOfWork.SaveChanges();
    }

    public async Task PutStatusComplaint(long userId, long complaintId, ComplaintImportance importance)
    {
        var complaint = await _unitOfWork
            .GetRepository<Complaint>()
            .FirstOrDefaultAsync(x => x.Id == complaintId);

        var userComplaint = await _unitOfWork
                                .GetRepository<UserComplaint>()
                                .FirstOrDefaultAsync(x => x.UserId == userId && x.ComplaintId == complaintId)
                            ?? new UserComplaint
                            {
                                UserId = userId,
                                ComplaintId = complaintId
                            };

        userComplaint.Importance = importance;

        switch (importance)
        {
            case ComplaintImportance.Like:
                complaint.CountLike += 1;
                break;
            case ComplaintImportance.Dislike:
                complaint.CountDislike += 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(importance), importance, null);
        }
    }

    public async Task<ComplaintData[]> GetComplaints(long? userId)
    {
        var complaints = await _unitOfWork.GetRepository<Complaint>()
            .GetAll()
            .ToArrayAsync();

        if (userId is null) return _mapper.Map<Complaint[], ComplaintData[]>(complaints);

        var userComplaint = await _unitOfWork.GetRepository<UserComplaint>()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var userComplaints = from cs in complaints
            join uc in userComplaint on cs.Id equals uc.ComplaintId
            select new ComplaintData()
            {
                Id = cs.Id,
                Author = new UserData(){PhoneNumber = _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x => x.Id == cs.AuthorId).Result.PhoneNumber},
                CountLike = cs.CountLike,
                CountDislike = cs.CountDislike,
                Date = cs.Date,
                ImageUrl = cs.ImageUrl,
                Importance = uc.Importance,
                Name = cs.Name,
                Description = cs.Description
            };
        return userComplaints.ToArray();
    }
}