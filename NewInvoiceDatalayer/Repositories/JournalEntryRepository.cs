using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories;

public class JournalEntryRepository : BaseRepository<DO_JournalEntry>, IJournalEntryRepository
{
    public JournalEntryRepository(IInvoiceDbContext dataContext) : base(dataContext)
    {
        _dataObjectTable = dataContext.JournalEntries;
    }

    public async Task<DO_JournalEntry> SaveJournalEntryAsync(DO_JournalEntry toCreate)
    {
        DO_JournalEntry created;

        try
        {
            created = await AddAsync(toCreate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return created;
    }
}