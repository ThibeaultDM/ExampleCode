using Microsoft.EntityFrameworkCore;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories;

public class InvoiceHeaderRepository : BaseRepository<DO_InvoiceHeader>, IInvoiceHeaderRepository
{
    public InvoiceHeaderRepository(IInvoiceDbContext invoiceDbContext) : base(invoiceDbContext)
    {
        _dataObjectTable = invoiceDbContext.InvoiceHeaders;
    }

    public async Task<DO_InvoiceHeader> CreateInvoiceHeaderAsync(DO_InvoiceHeader toCreate)
    {
        DO_InvoiceHeader created;

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

    public async Task<List<DO_InvoiceHeader>> GetInvoiceHeadersAsync()
    {
        List<DO_InvoiceHeader> invoiceHeaders = [];

        try
        {
            invoiceHeaders = await GetAllAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return invoiceHeaders;
    }

    public async Task<DO_InvoiceHeader> FindInvoiceHeaderAsync(Guid toFind)
    {
        DO_InvoiceHeader invoiceHeader;

        try
        {
            invoiceHeader = await _dataObjectTable.AsNoTracking().Include(h => h.InvoiceLines).SingleOrDefaultAsync(h => h.Id == toFind);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return invoiceHeader;
    }

    public async Task<DO_InvoiceHeader> UpdateInvoiceHeaderAsync(DO_InvoiceHeader toUpdate)
    {
        DO_InvoiceHeader UpDated;

        try
        {
            if (toUpdate.InvoiceLines != null && toUpdate.InvoiceLines.Count > 0)
            {
                await _dataContext.InvoiceLines.AddAsync(UpdateCreateProperties(toUpdate.InvoiceLines.Last()));
            }

            UpDated = await UpdateAsync(toUpdate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return UpDated;
    }

    public async Task<bool> DeleteInvoiceHeaderAsync(DO_InvoiceHeader toDelete)
    {
        bool success = false;

        try
        {
            // TODO probably need to clear tracker before the remove can be executed
            success = await DeleteAsync(toDelete);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return success;
    }
}