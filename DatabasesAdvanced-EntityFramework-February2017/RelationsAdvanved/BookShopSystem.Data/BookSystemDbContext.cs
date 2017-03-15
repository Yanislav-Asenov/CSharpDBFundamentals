namespace BookShopSystem.Data
{
    using BookShopSystem.Models;
    using System.Data.Entity;

    public class BookSystemDbContext : DbContext
    {
        public BookSystemDbContext()
            : base("name=BookSystemDbContext")
        {
        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Book> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.RelatedBooks)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("BookId");
                    m.MapRightKey("RelatedBookId");
                    m.ToTable("RelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}