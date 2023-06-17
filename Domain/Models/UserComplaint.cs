using Dal.interfaces;
using Domain.Enums;

namespace Domain.Models;

public class UserComplaint : IBaseEntity
{
    public long? UserId { get; set; }

    public long? ComplaintId { get; set; }

    public ComplaintImportance? Importance { get; set; }

    public User User { get; set; }

    public Complaint Complaint { get; set; }
}