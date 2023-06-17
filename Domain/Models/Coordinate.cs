namespace Domain.Models;

public class Coordinate : BaseEntity<long>
{
    public long? ComplaintId { get; set; }
    public long? RegionId { get; set; }
    public long? DistrictId { get; set; }
    public long? CityId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Complaint Complaint { get; set; }
    public City City { get; set; }
    public District District { get; set; }
    public Region Region { get; set; }
}