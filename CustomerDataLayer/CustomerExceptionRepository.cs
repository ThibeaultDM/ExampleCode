using CustomerDataLayer.DataModels;
using CustomerDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerDataLayer;

public class CustomerExceptionRepository : ICustomerExceptionRepository, IDisposable
{
    private CustomerDbContext _data;

    public CustomerExceptionRepository(CustomerDbContext dbContext)
    {
        _data = dbContext;
    }

    public async Task CreateExceptionAsync(DO_CustomerException exceptionToCreate)
    {
        exceptionToCreate.CreatedBy = Environment.UserName;
        exceptionToCreate.UpdatedBy = Environment.UserName;
        exceptionToCreate.CreatedOn = DateTime.Now;
        exceptionToCreate.UpdatedOn = DateTime.Now;
        await _data.CustomerExceptions.AddAsync(exceptionToCreate);
        await SaveAsync();
    }

    public async Task<List<DO_CustomerException>> GetAllExceptionsAsync()
    {
        return await _data.CustomerExceptions.ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _data.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _data.Dispose();
            }
        }
        disposed = true;
    }
}