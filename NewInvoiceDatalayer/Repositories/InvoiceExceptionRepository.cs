using Microsoft.EntityFrameworkCore;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories
{
    public class InvoiceExceptionRepository : BaseRepository, IInvoiceExceptionRepository
    {
        public InvoiceExceptionRepository(IInvoiceDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<DO_InvoiceException> SaveInvoiceExceptionAsync(DO_InvoiceException toCreate)
        {
            DO_InvoiceException created;

            try
            {
                toCreate = Create(toCreate);
                await TableInvoiceExceptions().AddAsync(toCreate);
                await SaveAsync();
                created = toCreate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return created;
        }

        #region Helper methodes

        private DbSet<DO_InvoiceException> TableInvoiceExceptions()
        {
            return _dataContext.InvoiceExceptions;
        }

        #endregion Helper methodes
    }
}