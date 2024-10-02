namespace Training_UI.Models.Input
{
    public class CreateInvoiceInput
    {
        public Guid ProxyId { get; set; }
        public string VatNumber { get; set; }

        public List<CreateInvoiceLineInput> InvoiceLines { get; set; } = new();
        public bool IsPaid { get; set; } = false;
    }
}