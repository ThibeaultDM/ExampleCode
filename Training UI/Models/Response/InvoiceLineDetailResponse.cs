namespace Training_UI.Models.Response

{
    public class InvoiceLineDetailResponse : BaseResponse
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string InvoiceNumber { get; set; }

        public bool IsActive { get; set; }

        public decimal VATAmount { get; set; }

        public string VatNumber { get; set; }
    }
}