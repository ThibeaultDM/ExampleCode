namespace Orchestration.Models.Response;

public class ArchiveInvoiceJournalEntryResponse : BaseResponse
{
    public Guid JournalHeaderId { get; set; }
    public Guid InvoiceHeaderId { get; set; }
}