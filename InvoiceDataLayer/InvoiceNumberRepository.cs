using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;

namespace InvoiceDataLayer
{
    public class InvoiceNumberRepository : IInvoiceNumberRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceNumberRepository(InvoiceDbContext dbContext)
        {
            this._context = dbContext;
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
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int GetNextNumber()
        {
            try
            {
                var invoiceNumber = _context.InvoiceNumber.FirstOrDefault();

                if (invoiceNumber == null)
                {
                    invoiceNumber = new DO_InvoiceNumber();
                    invoiceNumber.LastUsedNumber = 1;
                    _context.Entry<DO_InvoiceNumber>(invoiceNumber).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    invoiceNumber.LastUsedNumber += 1;
                    _context.Entry<DO_InvoiceNumber>(invoiceNumber).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }

                Save();
                return invoiceNumber.LastUsedNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}