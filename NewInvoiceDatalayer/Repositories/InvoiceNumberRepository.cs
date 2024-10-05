using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewInvoiceDataLayer.Repositories
{
    public class InvoiceNumberRepository : BaseRepository, IInvoiceNumberRepository
    {
        public InvoiceNumberRepository(IInvoiceDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<int> GetNextNumber()
        {
            try
            {
                var invoiceNumber = _dataContext.InvoiceNumber.FirstOrDefault();

                if (invoiceNumber == null)
                {
                    // TODO Load one in the database on initialization
                    invoiceNumber = new DO_InvoiceNumber();
                    invoiceNumber.LastUsedNumber = 1;
                    invoiceNumber = Create(invoiceNumber);
                    await _dataContext.InvoiceNumber.AddAsync(invoiceNumber);
                }
                else
                {
                    invoiceNumber.LastUsedNumber += 1;
                    invoiceNumber = Create(invoiceNumber);
                    _dataContext.Entry(invoiceNumber).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                await SaveAsync();
                return invoiceNumber.LastUsedNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
