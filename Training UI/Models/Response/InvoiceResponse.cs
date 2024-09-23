namespace Training_UI.Models.Response
{
    public class InvoiceResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public bool IsPaid { get; set; }
        public string VatNumber { get; set; }
    }
}