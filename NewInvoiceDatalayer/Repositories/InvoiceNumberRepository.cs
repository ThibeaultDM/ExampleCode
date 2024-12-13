using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories
{
    public class InvoiceNumberRepository : BaseRepository<DO_InvoiceNumber>, IInvoiceNumberRepository
    {
        public InvoiceNumberRepository(IInvoiceDbContext dataContext) : base(dataContext)
        {
            _dataObjectTable = dataContext.InvoiceNumber;
        }

        public async Task<int> GetNextNumber()
        {
            DO_InvoiceNumber invoiceNumber;

            try
            {
                invoiceNumber = _dataObjectTable.FirstOrDefault();

                if (invoiceNumber == null)
                {
                    // TODO Load one in the database on initialization
                    invoiceNumber = new DO_InvoiceNumber
                    {
                        LastUsedNumber = 1
                    };
                    invoiceNumber = await AddAsync(invoiceNumber);
                }
                else
                {
                    invoiceNumber.LastUsedNumber += 1;
                    invoiceNumber = await UpdateAsync(invoiceNumber);
                }

                return invoiceNumber.LastUsedNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}