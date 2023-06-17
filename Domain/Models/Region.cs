namespace Domain.Models;

public class Region : BaseEntity<long>
{
    public string? Name { get; set; }
    public IList<District> Districts { get; set; }
    public IList<City> Cities { get; set; }
}