namespace Hospital.Models.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Hospital.Models.HospitalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Hospital.Models.HospitalDbContext";
        }

        protected override void Seed(Hospital.Models.HospitalDbContext context)
        {
        }
    }
}
