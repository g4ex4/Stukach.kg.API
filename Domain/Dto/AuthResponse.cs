namespace Domain.Dto;

public class AuthResponse
{
    public AuthResponse(string message, long userId)
    {
        Message = message;
        UserId = userId;
    }

    public string Message { get; set; }
    public long UserId { get; set; }
}