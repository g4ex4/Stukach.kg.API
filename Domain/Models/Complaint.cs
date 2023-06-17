using Domain.Enums;

namespace Domain.Models;

public class Complaint : BaseEntity<long>
{
    public string Name { get; set; }

    public long AuthorId { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public int CountLike { get; set; }

    public int CountDislike { get; set; }

    public DateTime Date { get; set; }

    public ComplaintStatus Status { get; set; }

    public User Author { get; set; }

    public IList<UserComplaint> UserComplaints { get; set; }
}