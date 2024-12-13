using NewInvoiceCommunicationLayer.Models.Response;

namespace NewInvoiceCommunicationLayer.Models.Input;

public class ArchiveInvoiceJournalEntry : BaseResponse
{
    public Guid JournalHeaderId { get; set; }
    public Guid InvoiceHeaderId { get; set; }
}