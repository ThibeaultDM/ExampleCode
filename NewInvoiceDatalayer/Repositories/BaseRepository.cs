using Microsoft.EntityFrameworkCore;
using NewInvoiceDataLayer.Interfaces;
using QueasoFramework.DataModels;

namespace NewInvoiceDataLayer.Repositories;

public abstract class BaseRepository<T> where T : DataObjectBase
{
    private bool _disposed = false;
    protected DbSet<T> _dataObjectTable;
    protected IInvoiceDbContext _dataContext;

    protected BaseRepository(IInvoiceDbContext dbContext)
    {
        _dataContext = dbContext;
    }

    #region CRUD

    /// <summary>
    /// Add a DataObjectBase to the database
    /// </summary>
    /// <param name="toAdd"></param>
    /// <returns></returns>
    protected async Task<T> AddAsync(T toAdd)
    {
        T created;

        try
        {
            toAdd = UpdateCreateProperties(toAdd);
            await _dataObjectTable.AddAsync(toAdd);
            await SaveAsync();

            created = toAdd;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return created;
    }

    /// <summary>
    /// Updates a DataObjectBase
    /// </summary>
    /// <param name="toUpdate"></param>
    /// <returns></returns>
    protected async Task<T> UpdateAsync(T toUpdate)
    {
        T updated;
        try
        {
            toUpdate = UpdateCreateProperties(toUpdate);

            _dataContext.Entry(toUpdate).State = EntityState.Modified;
            await SaveAsync();

            updated = toUpdate;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return updated;
    }

    /// <summary>
    /// Gets all DataObjectBases
    /// </summary>
    /// <returns></returns>
    protected async Task<List<T>> GetAllAsync()
    {
        List<T> AllThings;

        try
        {
            AllThings = await _dataObjectTable.ToListAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return AllThings;
    }

    /// <summary>
    /// Deletes a DataObjectBase from the database
    /// </summary>
    /// <param name="toDelete"></param>
    /// <returns></returns>
    protected async Task<bool> DeleteAsync(T toDelete)
    {
        bool success = false;
        try
        {
            //toDelete = UpdateDeleteProperties(toDelete);
            _dataObjectTable.Remove(toDelete);
            await SaveAsync();

            success = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return success;
    }

    #endregion CRUD

    #region Helper methodes

    /// <summary>
    /// Updates the UpdatedOn and UpdatedBy property's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toUpdate"></param>
    /// <returns></returns>
    protected T UpdateUpdateProperties<T>(T toUpdate) where T : DataObjectBase
    {
        toUpdate.UpdatedOn = DateTime.Now;
        toUpdate.UpdatedBy = Environment.UserName;

        return toUpdate;
    }

    /// <summary>
    /// Updates the CreatedOn and CreatedBy property's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toUpdate"></param>
    /// <returns></returns>
    protected T UpdateCreateProperties<T>(T toCreate) where T : DataObjectBase
    {
        toCreate.CreatedOn = DateTime.Now;
        toCreate.CreatedBy = Environment.UserName;

        return toCreate;
    }

    /// <summary>
    /// Updates the DeletedOn and DeletedBy property's
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="toDelete"></param>
    /// <returns></returns>
    protected T UpdateDeleteProperties<T>(T toDelete) where T : DataObjectBase
    {
        toDelete.DeletedOn = DateTime.Now;
        toDelete.DeletedBy = Environment.UserName;

        return toDelete;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
        _disposed = true;
    }

    protected async Task SaveAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    #endregion Helper methodes
}