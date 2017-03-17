namespace Gringotts.Data
{
    using System.Data.Entity;

    public class GringottsDbContext : DbContext
    {
        public GringottsDbContext()
            : base("name=GringottsEntities")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<WizzardDeposit> WizzardDeposits { get; set; }
    }
}