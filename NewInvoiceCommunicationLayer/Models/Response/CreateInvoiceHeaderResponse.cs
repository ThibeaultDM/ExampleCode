namespace NewInvoiceCommunicationLayer.Models.Response;

public class CreateInvoiceHeaderResponse : BaseResponse
{
    public Guid Id { get; set; }
    public Guid CompanyProxyId { get; set; }

    /// <summary>
    /// Amount to be paid before taxes
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Amount to be paid with taxes
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Amount of taxes to be paid
    /// </summary>
    public decimal VatAmount { get; set; }

    public int InvoiceNumber { get; set; }
    public string VatNumber { get; set; }
    public bool IsPaid { get; set; }
}