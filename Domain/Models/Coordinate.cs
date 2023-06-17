namespace Domain.Models;

public class Coordinate : BaseEntity<long>
{
    public long? ComplaintId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Complaint Complaint { get; set; }
}