namespace CarDealer.Xml.Data
{
    using CarDealer.Xml.Models;
    using System.Data.Entity;

    public class CarDealerXmlDbContext : DbContext
    {
        public CarDealerXmlDbContext()
            : base("name=CarDealerXmlDbContext")
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Parts)
                .WithMany(p => p.Cars);

            modelBuilder.Entity<Part>()
                .HasRequired(p => p.Supplier)
                .WithMany(s => s.Parts);

            modelBuilder.Entity<Car>()
                .HasMany(c => c.Sales)
                .WithRequired(s => s.Car);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithRequired(s => s.Customer);

            base.OnModelCreating(modelBuilder);
        }
    }
}