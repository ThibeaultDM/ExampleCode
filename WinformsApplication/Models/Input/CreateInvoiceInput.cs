using System.ComponentModel;

namespace WinformsApplication.Models.Input
{
    public class CreateInvoiceInput
    {
        public Guid ProxyId { get; set; }
        public string VatNumber { get; set; }

        public BindingList<CreateInvoiceLine> InvoiceLines { get; set; } = new();
        public bool IsPaid { get; set; } = false;
    }
}