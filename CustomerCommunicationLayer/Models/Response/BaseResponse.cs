namespace InvoiceCommunicationLayer.Models.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<ErrorResponse>();
        }

        public bool Succes { get; set; } = true;
        public List<ErrorResponse> Errors { get; set; } = new();
    }
}