namespace Domain.Models;

public class Brigade : BaseEntity<long>
{
    public int BrigadeNumber { get; set; }
    public List<Complaint> Complaints { get; set; }
}