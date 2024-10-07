namespace Orchestration.Models.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<ErrorResponse>();
        }

        public bool Success { get; set; }
        public List<ErrorResponse> Errors { get; set; } = new();
    }
}