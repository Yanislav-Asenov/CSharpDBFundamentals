namespace LocalStore.Models
{
    using System.Data.Entity;

    public class LocalStoreDbContext : DbContext
    {
        public LocalStoreDbContext()
            : base("name=LocalStoreDbContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<LocalStoreDbContext>());
        }

        public DbSet<Product> Products { get; set; }
    }
}