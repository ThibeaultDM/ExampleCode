namespace ModuleInvoice.Models.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
        public List<ErrorResponse> Errors { get; set; } = [];
    }
}