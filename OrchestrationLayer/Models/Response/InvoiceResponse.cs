namespace Orchestration.Models.Response
{
    public class InvoiceResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public Guid? ProxyIdCompany { get; set; }
        public bool IsPaid { get; set; }
        public string VatNumber { get; set; }
    }
}