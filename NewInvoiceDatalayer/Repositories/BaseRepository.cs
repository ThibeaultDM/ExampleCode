using NewInvoiceDataLayer.Interfaces;
using QueasoFramework.DataModels;

namespace NewInvoiceDataLayer.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private bool _disposed = false;
        protected readonly IInvoiceDbContext _dataContext;

        protected BaseRepository(IInvoiceDbContext dataContext)
        {
            this._dataContext = dataContext;
        }

        /// <summary>
        /// Updates the UpdatedOn and UpdatedBy property's
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toUpdate"></param>
        /// <returns></returns>
        protected T Update<T>(T toUpdate) where T : DataObjectBase
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
        protected T Create<T>(T toCreate) where T : DataObjectBase
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
        protected T Delete<T>(T toDelete) where T : DataObjectBase
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
            this._disposed = true;
        }

        protected async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}