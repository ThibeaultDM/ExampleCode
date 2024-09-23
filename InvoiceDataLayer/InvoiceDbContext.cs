using InvoiceDataLayer.DataModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceDataLayer
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext()
        { }

        public InvoiceDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<DO_InvoiceException> InvoiceException { get; set; }

        public DbSet<DO_InvoiceHeader> InvoiceHeader { get; set; }

        public DbSet<DO_InvoiceLine> InvoiceLine { get; set; }

        public DbSet<DO_InvoiceNumber> InvoiceNumber { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DO_InvoiceHeader>().HasIndex(i => i.Id).IsUnique();
            modelBuilder.Entity<DO_InvoiceLine>().HasIndex(i => i.Id).IsUnique();
            modelBuilder.Entity<DO_InvoiceLine>().HasOne<DO_InvoiceHeader>().WithMany(i => i.InvoiceLines);

            modelBuilder.Entity<DO_InvoiceHeader>().ToTable("InvoiceHeader");
            modelBuilder.Entity<DO_InvoiceLine>().ToTable("InvoiceLine");

            base.OnModelCreating(modelBuilder);
        }
    }
}