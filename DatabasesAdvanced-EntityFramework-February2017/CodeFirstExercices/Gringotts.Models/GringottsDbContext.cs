namespace Gringotts.Models
{
    using System.Data.Entity;

    public class GringottsDbContext : DbContext
    {
        public GringottsDbContext() : base("GringottsDB")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<GringottsDbContext>());
        }

        public DbSet<WizardDeposit> WizzardDeposits { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
