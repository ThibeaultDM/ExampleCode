using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewInvoiceDataLayer.Interfaces;
using NewInvoiceDataLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
        public override void Dispose() => base.Dispose();

        public override EntityEntry Entry(object entity) => base.Entry(entity);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);

    }
}
