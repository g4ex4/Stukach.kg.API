using System.Text;

namespace Domain.Models;

public class Coordinate : BaseEntity<long>
{
    public long ComplaintId { get; set; }
    public long? RegionId { get; set; }
    public long? DistrictId { get; set; }
    public long? CityId { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public Complaint Complaint { get; set; }
    public City? City { get; set; }
    public District? District { get; set; }
    public Region? Region { get; set; }

    public string GetFullAddress()
    {
        var result = new StringBuilder();
        if (Region is not null) result.Append(Region.Name);
        if (District is not null) result.Append(District.Name);
        if (City is not null) result.Append(City.Name);
        return result.ToString();
    }
}