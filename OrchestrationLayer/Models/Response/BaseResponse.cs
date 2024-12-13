namespace Orchestration.Models.Response;

public class BaseResponse
{
    public BaseResponse()
    {
        Errors = [];
    }

    public bool Success { get; set; }
    public List<ErrorResponse> Errors { get; set; } = [];
}