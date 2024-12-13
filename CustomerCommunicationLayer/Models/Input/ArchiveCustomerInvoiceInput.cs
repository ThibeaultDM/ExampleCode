namespace CustomerCommunicationLayer.Models.Input;

public class ArchiveCustomerInvoiceInput
{
    public Guid CustomerId { get; set; }
    public Guid InvoiceHeaderId { get; set; }
    public Guid JournalEntryId { get; set; }
    public int Number { get; set; }
    public string PublicName { get; set; }
    public string StreetName { get; set; }
}