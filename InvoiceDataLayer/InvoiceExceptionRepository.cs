using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDataLayer
{
    public class InvoiceExceptionRepository : IInvoiceExceptionRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceExceptionRepository(InvoiceDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task CreateInvoiceExceptionsAsync(DO_InvoiceException record)
        {
            record.CreatedBy = Environment.UserName;
            record.UpdatedBy = Environment.UserName;
            record.CreatedOn = DateTime.Now;
            record.UpdatedOn = DateTime.Now;
            await _context.InvoiceException.AddAsync(record);
            await SaveAsync();
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

        public async Task<IEnumerable<DO_InvoiceException>> GetExceptionsAsync()
        {
            return await _context.InvoiceException.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}