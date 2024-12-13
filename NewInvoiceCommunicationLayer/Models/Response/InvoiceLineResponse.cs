namespace NewInvoiceCommunicationLayer.Models.Response;

public class InvoiceLineResponse : BaseResponse
{
    public Guid Id { get; set; }

    public decimal Amount { get; set; }

    public decimal VATAmount { get; set; }

    public decimal VATRate { get; set; }

    public decimal LineAmount { get; set; }

    public decimal PricePerUnit { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }
}