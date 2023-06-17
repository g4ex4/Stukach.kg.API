namespace Domain.Dto;

public class AuthResponse
{
    public AuthResponse(string message)
    {
        Message = message;
    }
    public string Message { get; set; }
}