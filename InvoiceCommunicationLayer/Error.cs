namespace InvoiceCommunicationLayer
{
    public class Error
    {
        public string RequestId { get; set; }

        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}