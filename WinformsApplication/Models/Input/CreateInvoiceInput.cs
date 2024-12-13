using System.ComponentModel;

namespace WinFormsApplication.Models.Input;

public class CreateInvoiceInput
{
    public Guid ProxyId { get; set; }
    public string VatNumber { get; set; }

    public BindingList<CreateInvoiceLine> InvoiceLines { get; set; } = [];
    public bool IsPaid { get; set; } = false;
}