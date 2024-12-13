using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Interfaces;

public interface IJournalEntryRepository
{
    Task<DO_JournalEntry> SaveJournalEntryAsync(DO_JournalEntry toCreate);
}