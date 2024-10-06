using Microsoft.EntityFrameworkCore;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Repositories
{
    public class InvoiceHeaderRepository : BaseRepository, IInvoiceHeaderRepository
    {
        public InvoiceHeaderRepository(IInvoiceDbContext invoiceDbContext) : base(invoiceDbContext)
        {
        }

        public async Task<DO_InvoiceHeader> CreateInvoiceHeaderAsync(DO_InvoiceHeader toCreate)
        {
            DO_InvoiceHeader created;

            try
            {
                toCreate = Update(toCreate);

                await TableInvoiceHeader().AddAsync(toCreate);
                await SaveAsync();

                created = toCreate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return created;
        }

        public async Task<List<DO_InvoiceHeader>> GetInvoiceHeadersAsync()
        {
            List<DO_InvoiceHeader> invoiceHeaders = new();

            try
            {
                invoiceHeaders = await TableInvoiceHeader().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return invoiceHeaders;
        }

        public async Task<DO_InvoiceHeader> FindInvoiceHeaderAsync(Guid toFind)
        {
            DO_InvoiceHeader invoiceHeader;

            try
            {
                invoiceHeader = await TableInvoiceHeader().AsNoTracking().Include(h => h.InvoiceLines).SingleOrDefaultAsync(h => h.Id == toFind);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return invoiceHeader;
        }

        public async Task<DO_InvoiceHeader> UpdateInvoiceHeaderAsync(DO_InvoiceHeader toUpdate)
        {
            DO_InvoiceHeader UpDated;

            try
            {
                toUpdate = Update(toUpdate);

                if (toUpdate.InvoiceLines.Count > 0)
                {
                    await _dataContext.InvoiceLines.AddAsync(Update(toUpdate.InvoiceLines.Last()));
                }

                _dataContext.Entry(toUpdate).State = EntityState.Modified;
                await SaveAsync();

                UpDated = toUpdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return UpDated;
        }

        public async Task<bool> DeleteInvoiceHeaderAsync(DO_InvoiceHeader toDelete)
        {
            bool succes = false;

            try
            {
                // TODO probably need to clear tracker before the remove can be executed
                TableInvoiceHeader().Remove(toDelete);
                await SaveAsync();
                succes = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return succes;
        }

        #region Helper Methodes

        private DbSet<DO_InvoiceHeader> TableInvoiceHeader()
        {
            return _dataContext.InvoiceHeaders;
        }

        #endregion Helper Methodes
    }
}