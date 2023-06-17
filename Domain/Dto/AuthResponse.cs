using Domain.Models;

namespace Domain.Dto;

public class AuthResponse : Response
{
    public AuthResponse(int statusCode, string message, bool success, long userId)
        : base (statusCode, message, success)
    {
        UserId = userId;
    }

    
    public long UserId { get; set; }
}