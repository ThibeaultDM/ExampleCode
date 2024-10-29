namespace NewInvoiceCommunicationLayer.Models.Input
{
    public class ArchiveInvoiceJournalEntry
    {
        public Guid JournalHeaderId { get; set; }
        public Guid InvoiceHeaderId { get; set; }
    }
}