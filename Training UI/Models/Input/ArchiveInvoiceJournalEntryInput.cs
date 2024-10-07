namespace Training_UI.Models.Input
{
    public class ArchiveInvoiceJournalEntryInput
    {
        public ArchiveInvoiceJournalEntryInput(Guid journalEntryId, Guid journalHeaderId)
        {
            proxyCompanyId = journalEntryId;
            InvoiceHeaderId = journalHeaderId;
        }

        public Guid proxyCompanyId { get; set; }
        public Guid InvoiceHeaderId { get; set; }
    }
}