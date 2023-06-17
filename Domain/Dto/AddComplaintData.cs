using Microsoft.AspNetCore.Http;

namespace Domain.Dto;

public class AddComplaintData
{
    public long UserId { get; set; }
    
    public string Name { get; set; }

    public IFormFile Image { get; set; }

    public DateTime Date { get; set; }

    public CoordinateData CoordinateData { get; set; }
}