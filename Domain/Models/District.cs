namespace Domain.Models;

public class District : BaseEntity<long>
{
    public long? RegionId { get; set; }
    public string Name { get; set; }
    public virtual Region Region { get; set; }
    public virtual IList<City> Cities { get; set; }
}