using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer.Interfaces
{
    public interface IInvoiceDbContext
    {
        DbSet<DO_InvoiceException> InvoiceExceptions { get; set; }
        DbSet<DO_InvoiceHeader> InvoiceHeaders { get; set; }
        DbSet<DO_InvoiceLine> InvoiceLines { get; set; }
        DbSet<DO_InvoiceNumber> InvoiceNumber { get; set; }

        Task<int> SaveChangesAsync();

        void Dispose();

        EntityEntry Entry(object entity);
    }
}