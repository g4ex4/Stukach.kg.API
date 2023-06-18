using Domain.Common;

namespace Domain.Dto;

public class BrigadeResponse : Response
{
    public BrigadeResponse(int statusCode, string message, bool isSuccess, long bridageId)
        : base(statusCode, message, isSuccess)
    {
        BridageId = bridageId;
    }

    public long BridageId { get; set; }
}