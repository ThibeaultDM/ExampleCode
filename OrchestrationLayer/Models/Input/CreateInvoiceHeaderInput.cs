namespace Orchestration.Models.Input;

public class CreateInvoiceHeaderInput
{
    public CreateInvoiceHeaderInput(string vatNumber, Guid proxyCompanyId)
    {
        VATNumber = vatNumber;
        ProxyCompanyId = proxyCompanyId;
    }

    public string VATNumber { get; set; }

    public Guid ProxyCompanyId { get; set; }
}