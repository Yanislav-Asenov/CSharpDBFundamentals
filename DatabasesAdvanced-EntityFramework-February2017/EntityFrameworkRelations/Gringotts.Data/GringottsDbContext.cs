namespace Gringotts.Data
{
    using Model;
    using System.Data.Entity;

    public class GringottsDbContext : DbContext
    {
        public GringottsDbContext()
            : base("name=GringottsDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<User> Users { get; set; }
    }
}