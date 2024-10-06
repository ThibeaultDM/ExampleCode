namespace NewInvoiceCommunicationLayer.Models.Response
{
    public class ErrorResponse
    {
        public ErrorResponse(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}