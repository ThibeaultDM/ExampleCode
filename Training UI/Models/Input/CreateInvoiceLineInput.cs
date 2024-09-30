namespace Training_UI.Models.Input
{
    public class CreateInvoiceLineInput
    {
        public Guid InvoiceHeaderId { get; set; }

        public decimal VATRate { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; } = 0;

        public string Description { get; set; }
    }
}