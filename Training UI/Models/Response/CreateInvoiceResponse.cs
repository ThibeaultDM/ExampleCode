namespace Training_UI.Models.Response
{
    public class CreateInvoiceResponse : InvoiceLineResponse
    {
        public decimal VATRate { get; set; }

        public decimal LineAmount { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; } = 0;

        public string Description { get; set; }
    }
}