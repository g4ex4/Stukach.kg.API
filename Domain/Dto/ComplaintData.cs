using Domain.Enums;

namespace Domain.Dto;

public class ComplaintData
{
    public long Id { get; set; }

    public string Name { get; set; }
    
    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public int CountLike { get; set; }

    public int CountDislike { get; set; }

    public DateTime Date { get; set; }

    public ComplaintStatus Status { get; set; }

    public ComplaintImportance Importance { get; set; }

    public UserData Author { get; set; }
}