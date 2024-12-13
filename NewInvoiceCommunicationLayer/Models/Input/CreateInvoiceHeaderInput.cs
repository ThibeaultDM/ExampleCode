namespace NewInvoiceCommunicationLayer.Models.Input;

public class CreateInvoiceHeaderInput
{
    public string VATNumber { get; set; }

    public string ProxyCompanyId { get; set; }
}