namespace NewInvoiceCommunicationLayer.Models.Response;

public class BaseResponse
{
    public bool Success { get; private set; } = true;
    public List<ErrorResponse>? Errors { get; private set; }

    public void SetErrors(ErrorResponse error)
    {
        Success = false;

        if (Errors == null)
        {
            Errors = [];
        }

        Errors.Add(error);
    }
}