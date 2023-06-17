using Microsoft.AspNetCore.Http;

namespace Domain.Dto;

public class AddComplaintData
{
    public long? UserId { get; set; }

    public string? Name { get; set; }

    public string? Image { get; set; }

    public string? Description { get; set; }

    public DateTime? Date { get; set; }

    public CoordinateData? Coordinate { get; set; }
}