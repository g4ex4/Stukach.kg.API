namespace Domain.Models;

public class City : BaseEntity<long>
{
    public long? DistrictId { get; set; }
    public long? RegionId { get; set; }
    public string? Name { get; set; }
    public virtual District District { get; set; }
    public virtual Region Region { get; set; }
}