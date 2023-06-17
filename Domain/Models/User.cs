namespace Domain.Models;

public class User : BaseEntity<long>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}