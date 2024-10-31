namespace Orchestration.Models.Input
{
    public class ArchiveInvoiceJournalEntryInput
    {
        public ArchiveInvoiceJournalEntryInput(Guid journalEntryId, Guid journalHeaderId)
        {
            JournalHeaderId = journalEntryId;
            InvoiceHeaderId = journalHeaderId;
        }

        public Guid JournalHeaderId { get; set; }
        public Guid InvoiceHeaderId { get; set; }
    }
}