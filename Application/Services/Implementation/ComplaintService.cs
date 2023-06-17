using Application.Integrations.Geocoding;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dal.interfaces;
using Domain.Common;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using FileIO = System.IO.File;

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

    public async Task<Response> AddComplaint(AddComplaintData complaint)
    {
        var newComplaint = new Complaint();
        newComplaint.Date = complaint.Date;
        newComplaint.Name = complaint.Name;
        newComplaint.AuthorId = complaint.UserId;
        newComplaint.Description = complaint.Description;
        newComplaint.Status = ComplaintStatus.Created;

        var fileName = Guid.NewGuid() + ".jpeg";
        var fullPath = Path.Combine("wwwroot", "Photos", fileName);
        var bytes = Convert.FromBase64String(complaint.Image);
        await FileIO.WriteAllBytesAsync(fullPath, bytes);

        newComplaint.ImageUrl = fileName;
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
        await _unitOfWork.GetRepository<Coordinate>().AddAsync(newComplaint.Coordinate);
        await _unitOfWork.GetRepository<Complaint>().AddAsync(newComplaint);
        await _unitOfWork.SaveChanges();
        return new Response(200, "Жалоба успешно отправлена!", true);
    }

    public async Task<Response> PutComplaintImportance(long userId, long complaintId, ComplaintImportance importance)
    {
        var complaint = await _unitOfWork
            .GetRepository<Complaint>()
            .Include(x => x.UserComplaints)
            .FirstOrDefaultAsync(x => x.Id == complaintId);
        if (complaint == null)
        {
            throw new NotFoundException(typeof(Complaint).ToString(), complaintId);
        }

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

        complaint.UserComplaints.Add(userComplaint);
        _unitOfWork.GetRepository<Complaint>().Update(complaint);
        await _unitOfWork.SaveChanges();
        return new Response(200, "Изменено успешно", true);
    }

    public async Task<ComplaintData[]> GetComplaints(long? userId)
    {
        var complaints = await _unitOfWork.GetRepository<Complaint>()
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.Region)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.District)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.City)
            .Include(x => x.Author)
            .OrderBy(x => x.CountLike)
            .ThenByDescending(x => x.CountDislike)
            .ProjectTo<ComplaintData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();

        if (userId is null) return complaints;

        var userComplaint = await _unitOfWork.GetRepository<UserComplaint>()
            .Where(x => x.UserId == userId)
            .ToListAsync();

        foreach (var userC in userComplaint)
        {
            foreach (var complaint in complaints)
            {
                if (userC.ComplaintId == complaint.Id)
                    complaint.Importance = userC.Importance;
            }
        }

        return complaints;
    }

    public async Task<Response> PutComplaintStatus(long complaintId, ComplaintStatus status)
    {
        var complaint = await _unitOfWork
            .GetRepository<Complaint>()
            .FirstOrDefaultAsync(x => x.Id == complaintId);
        if (complaint == null)
        {
            throw new NotFoundException(typeof(Complaint).ToString(), complaintId);
        }

        complaint.Status = status;
        _unitOfWork.GetRepository<Complaint>().Update(complaint);
        await _unitOfWork.SaveChanges();
        return new Response(200, "Статус изменен успешно!", true);
    }

    public async Task<ComplaintData[]> GetComplaintsByStatus(ComplaintStatus status)
    {
        var complaints = await _unitOfWork.GetRepository<Complaint>()
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.Region)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.District)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.City)
            .Include(x => x.Author)
            .Where(x => x.Status == status)
            .OrderBy(x => x.CountLike)
            .ThenByDescending(x => x.CountDislike)
            .ProjectTo<ComplaintData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();

        return complaints;
    }

    public async Task<ComplaintData> GetComplaintsById(long complainId)
    {
        var complaint = await _unitOfWork.GetRepository<Complaint>()
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.Region)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.District)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.City)
            .Include(x => x.Author)
            .ProjectTo<ComplaintData>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == complainId);
        return complaint;
    }

    public async Task<ComplaintData[]> GetComplaintByAddress(long? regionId, long? districtId, long? cityId)
    {
        var complaints = await _unitOfWork.GetRepository<Complaint>()
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.Region)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.District)
            .Include(x => x.Coordinate)
            .ThenInclude(x => x.City)
            .Include(x => x.Author)
            .Where(x => x.Coordinate.RegionId == regionId 
                        && (districtId == null || x.Coordinate.DistrictId == districtId )
                        && (cityId == null || x.Coordinate.CityId == cityId))
            .OrderBy(x => x.CountLike)
            .ThenByDescending(x => x.CountDislike)
            .ProjectTo<ComplaintData>(_mapper.ConfigurationProvider)
            .ToArrayAsync();


        return complaints;
    }
}