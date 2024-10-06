namespace NewInvoiceCommunicationLayer.Models.Input
{
    public class ArchiveInvoiceJournalEntryInput
    {
        public Guid proxyCompanyId { get; set; }
        public Guid InvoiceHeaderId { get; set; }
    }
}