namespace BlazorUI.Models.Response;

public class BaseResponse
{
    public BaseResponse()
    {
        Errors = [];
    }

    public bool Success { get; set; } = true;
    public List<ErrorResponse> Errors { get; set; } = [];
}