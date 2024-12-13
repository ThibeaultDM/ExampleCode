using CustomerDataLayer.DataModels;
using CustomerDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerDataLayer;

public class CustomerRepository : ICustomerRepository, IDisposable
{
    private CustomerDbContext _data;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        this._data = dbContext;
    }

    public async Task<DO_Customer> CreateCustomerAsync(DO_Customer customerToCreate)
    {
        customerToCreate.CreatedBy = Environment.UserName;
        customerToCreate.UpdatedBy = Environment.UserName;
        customerToCreate.CreatedOn = DateTime.Now;
        customerToCreate.UpdatedOn = DateTime.Now;
        await _data.Customers.AddAsync(customerToCreate);
        await SaveAsync();

        return customerToCreate;
    }

    public async Task<bool> DeleteCustomerByIdAsync(Guid id)
    {
        bool isDeleted;
        DO_Customer customerToDelete = await _data.Customers.SingleOrDefaultAsync(c => c.Id == id);

        if (customerToDelete != null)
        {
            try
            {
                _data.Customers.Remove(customerToDelete);
                SaveAsync();
                isDeleted = true;
            }
            catch (Exception)
            {
                isDeleted = false;
            }
        }
        else
        {
            isDeleted = false;
        }
        return isDeleted;
    }

    public async Task<List<DO_Customer>> GetAllCustomersAsync()
    {
        return await _data.Customers.ToListAsync();
    }

    public async Task<DO_Customer> GetCustomerByIdAsync(Guid id)
    {
        return await _data.Customers.AsNoTracking().Include(c => c.Addresses).Include(c => c.Company)
                                    .SingleOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
    }

    public async Task<DO_Customer> UpdateCustomerAsync(DO_Customer customerToUpdate)
    {
        customerToUpdate.UpdatedBy = Environment.UserName;
        customerToUpdate.UpdatedOn = DateTime.Now;
        customerToUpdate.DeletedOn = null;
        customerToUpdate.DeletedBy = null;
        _data.Entry<DO_Customer>(customerToUpdate).State = EntityState.Modified;
        await SaveAsync();

        return customerToUpdate;
    }

    public async Task SaveAsync()
    {
        await _data.SaveChangesAsync();
    }

    // TODO look up disposable agian and look at patern
    // for container disposes database connection (basic)
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
        this.disposed = true;
    }
}