using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories
{
    public class InvoiceExceptionRepository : BaseRepository<DO_InvoiceException>, IInvoiceExceptionRepository
    {
        public InvoiceExceptionRepository(IInvoiceDbContext dataContext) : base(dataContext)
        {
            _dataObjectTable = dataContext.InvoiceExceptions;
        }

        public async Task<DO_InvoiceException> SaveInvoiceExceptionAsync(DO_InvoiceException toCreate)
        {
            DO_InvoiceException created;

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
}