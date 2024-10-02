namespace Orchestration.Models.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Errors = new List<ErrorResponse>();
        }

        public bool Succes { get; set; }
        public List<ErrorResponse> Errors { get; set; } = new();
    }
}