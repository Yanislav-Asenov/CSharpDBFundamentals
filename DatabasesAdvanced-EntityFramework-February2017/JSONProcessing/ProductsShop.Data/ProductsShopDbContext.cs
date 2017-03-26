namespace ProductsShop.Data
{
    using ProductsShop.Models;
    using System.Data.Entity;

    public class ProductsShopDbContext : DbContext
    {
        public ProductsShopDbContext()
            : base("name=ProductsShopDbContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products);

            modelBuilder.Entity<Product>()
                .HasOptional(p => p.Seller)
                .WithMany(u => u.ProductsSold);

            modelBuilder.Entity<Product>()
                .HasOptional(p => p.Buyer)
                .WithMany(u => u.ProductsBought);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }
    }
}