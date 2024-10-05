using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;

namespace NewInvoiceDataLayer
{
    public class InvoiceDbContext : DbContext, IInvoiceDbContext
    {
        public InvoiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DO_InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<DO_InvoiceLine> InvoiceLines { get; set; }
        public DbSet<DO_InvoiceException> InvoiceExceptions { get; set; }
        public DbSet<DO_InvoiceNumber> InvoiceNumber { get; set; }

        #region Exposed for interface

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public override void Dispose() => base.Dispose();

        public override EntityEntry Entry(object entity) => base.Entry(entity);

        #endregion Exposed for interface

        #region Exposed for proper functioning of class

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

        #endregion Exposed for proper functioning of class
    }
}