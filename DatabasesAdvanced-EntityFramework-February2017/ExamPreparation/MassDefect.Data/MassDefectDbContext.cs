namespace MassDefect.Data
{
    using MassDefect.Data.Configurations;
    using MassDefect.Models;
    using System.Data.Entity;

    public class MassDefectDbContext : DbContext
    {
        public MassDefectDbContext()
            : base("name=MassDefectDbContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<MassDefectDbContext>());
        }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Planet> Plantes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SolarSystemConfiguration());
            modelBuilder.Configurations.Add(new StarConfiguration());
            modelBuilder.Configurations.Add(new PlanetConfiguration());
            modelBuilder.Configurations.Add(new AnomalyConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}