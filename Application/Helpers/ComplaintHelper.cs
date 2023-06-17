using Dal.interfaces;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers;

public static class ComplaintHelper
{
    public static async Task<int> GetCountImportance(IUnitOfWork unitOfWork, long complaintId, ComplaintImportance? type)
    {
        var userComplaints = await unitOfWork.GetRepository<UserComplaint>()
            .Where(x => x.ComplaintId == complaintId && x.Importance == type)
            .ToArrayAsync();

        return userComplaints.Length;
    }
}