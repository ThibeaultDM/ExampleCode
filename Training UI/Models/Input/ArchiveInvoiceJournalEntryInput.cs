namespace Training_UI.Models.Input
{
    public class ArchiveInvoiceJournalEntryInput
    {
        public ArchiveInvoiceJournalEntryInput(Guid journalEntryId, Guid journalHeaderId)
        {
            JournalEntryId = journalEntryId;
            JournalHeaderId = journalHeaderId;
        }

        public Guid JournalEntryId { get; set; }
        public Guid JournalHeaderId { get; set; }
    }
}