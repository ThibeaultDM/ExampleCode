using CustomerDataLayer.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CustomerDataLayer
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
        { }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<DO_Person> Persons { get; set; }
        public DbSet<DO_Address> Address { get; set; }

        public DbSet<DO_Customer> Customers { get; set; }
        public DbSet<DO_Company> Company { get; set; }
        public DbSet<DO_CreditInfo> CreditInfo { get; set; }
        public DbSet<DO_CustomerException> CustomerExceptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<DO_Person>().ToTable("Customer");
            mb.Entity<DO_Company>().ToTable("Company");
            mb.Entity<DO_Address>().ToTable("Address");
            mb.Entity<DO_CreditInfo>().ToTable("CreditInfo");

            mb.Entity<DO_Person>().HasIndex(p => p.Id).IsUnique();
            mb.Entity<DO_Company>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<DO_Address>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<DO_Address>().HasKey(c => c.Id);

            mb.Entity<DO_CreditInfo>().HasIndex(c => c.Id).IsUnique();
            mb.Entity<DO_CreditInfo>().HasKey(c => c.Id);

            // Customer - Company : one-to-zero-or-one
            mb.Entity<DO_Customer>()
                .HasOne(c => c.Company)
                .WithOne(co => co.Customer)
                .HasForeignKey<DO_Company>(co => co.Id);

            // Customer - Creditinfo : One-to-one
            mb.Entity<DO_Customer>()
                .HasOne(c => c.CreditInfo)
                .WithOne(cr => cr.Customer)
                .HasForeignKey<DO_CreditInfo>(ci => ci.Id);

            // Person - Address : required many-to-many
            mb.Entity<DO_Person>().HasKey(pa => new { pa.Id });
            // Person - Address : many to many
            mb.Entity<DO_Person>()
                .HasMany(a => a.Addresses)
                .WithMany(p => p.People)
                .UsingEntity(j => j.ToTable("PersonAddress"));

            mb.Entity<DO_Person>()
                .Navigation(p => p.Addresses)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            mb.Entity<DO_Address>()
                .Navigation(a => a.People)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            //Company - Address : required many-to-many
            mb.Entity<DO_Company>().HasKey(ca => new { ca.Id });
            //Company - Address : one to many
            mb.Entity<DO_Company>()
                .HasMany(a => a.Addresses)
                .WithMany(c => c.Companies)
                .UsingEntity(j => j.ToTable("CompanyAddress"));

            mb.Entity<DO_Company>()
                .Navigation(p => p.Addresses)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            mb.Entity<DO_Address>()
                .Navigation(a => a.Companies)
                .UsePropertyAccessMode(PropertyAccessMode.Property);

            // Company - Customer : required one-to-one
            mb.Entity<DO_Company>()
                .HasOne(c => c.Customer)
                .WithOne(co => co.Company)
                .HasForeignKey<DO_Company>(co => co.Id);

            base.OnModelCreating(mb);
        }
    }
}