namespace Sales.Models
{
    using System.Data.Entity;

    public class SalesDbContext : DbContext
    {
        public SalesDbContext()
            : base("name=SalesDbContext")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<StoreLocation> StoreLocations { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}