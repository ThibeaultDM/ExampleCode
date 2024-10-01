using InvoiceDataLayer.DataModels;
using InvoiceDataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDataLayer
{
    public class InvoiceHeaderRepository : IInvoiceHeaderRepository
    {
        private readonly InvoiceDbContext _context;

        public InvoiceHeaderRepository(InvoiceDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<DO_InvoiceHeader> CreateInvoiceHeaderAsync(DO_InvoiceHeader record)
        {
            record.CreatedBy = Environment.UserName;
            record.UpdatedBy = Environment.UserName;
            record.CreatedOn = DateTime.Now;
            record.UpdatedOn = DateTime.Now;
            await _context.InvoiceHeader.AddAsync(record);
            await SaveAsync();

            return record;
        }

        public async Task<List<DO_InvoiceHeader>> GetInvoiceHeadersAsync()
        {
            List<DO_InvoiceHeader> invoiceHeaders = await _context.InvoiceHeader.ToListAsync();

            return invoiceHeaders;
        }

        /// <summary>
        /// Gets specific Invoice by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<DO_InvoiceHeader> GetInvoiceHeaderAsync(Guid id)
        {
            DO_InvoiceHeader invoiceHeader = await _context.InvoiceHeader.Include(x => x.InvoiceLines).AsNoTracking().SingleOrDefaultAsync(h => h.Id == id && h.IsDeleted == false) ?? throw new Exception("header was not found"); ;

            return invoiceHeader;
        }

        public async Task UpdateInvoiceHeaderAsync(DO_InvoiceHeader record)
        {
            if (record.InvoiceLines.Count != 0)
            {
                record.InvoiceLines.Last().CreatedBy = Environment.UserName;
                record.InvoiceLines.Last().CreatedOn = DateTime.Now;
                record.InvoiceLines.Last().UpdatedBy = null;
                record.InvoiceLines.Last().UpdatedOn = null;
            }

            record.UpdatedOn = DateTime.Now;
            record.UpdatedBy = Environment.UserName;
            record.DeletedOn = null;
            record.DeletedBy = null;

            //await _context.InvoiceLine.AddAsync(record.InvoiceLines.Last());

            var entry = _context.InvoiceHeader.Attach(record);
            entry.State = EntityState.Modified;

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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}