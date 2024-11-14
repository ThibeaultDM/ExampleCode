namespace ModuleInvoice.Models.Input
{
    public class CreateInvoiceLineInput
    {
        public Guid InvoiceHeaderId { get; set; }

        public decimal VATRate { get; set; }

        public decimal PricePerUnit { get; set; }

        public int Quantity { get; set; } = 0;

        public string Description { get; set; } = "Description";

        public override string ToString()
        {
            return $"Description: {Description}, quantity: {Quantity}, Price per unit: {PricePerUnit}, VAT: {VATRate}";
        }
    }
}