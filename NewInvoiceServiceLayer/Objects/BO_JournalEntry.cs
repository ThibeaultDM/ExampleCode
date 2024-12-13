using QueasoFramework.BusinessModels;

namespace NewInvoiceBusinessLayer.Objects;

public class BO_JournalEntry : BusinessObjectBase
{
    public BO_JournalEntry(Guid journalHeaderId, Guid invoiceHeaderId)
    {
        JournalHeaderId = journalHeaderId;
        InvoiceHeaderId = invoiceHeaderId;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid JournalHeaderId { get; set; }
    public Guid InvoiceHeaderId { get; set; }
}