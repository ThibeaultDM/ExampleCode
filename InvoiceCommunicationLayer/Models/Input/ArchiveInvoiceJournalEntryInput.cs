namespace InvoiceCommunicationLayer.Models.Input
{
    public class ArchiveInvoiceJournalEntryInput
    {
        public Guid JournalEntryId { get; set; }
        public Guid JournalHeaderId { get; set; }
    }
}