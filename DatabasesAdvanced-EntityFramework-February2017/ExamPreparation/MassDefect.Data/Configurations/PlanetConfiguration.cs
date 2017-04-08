namespace MassDefect.Data.Configurations
{
    using MassDefect.Models;
    using System.Data.Entity.ModelConfiguration;

    public class PlanetConfiguration : EntityTypeConfiguration<Planet>
    {
        public PlanetConfiguration()
        {
            // Set primary key.
            this.HasKey(p => p.Id);

            // Set required fields.
            this.Property(p => p.Name).IsRequired();

            // Set relationships.
            this.HasRequired(p => p.Sun).WithMany(s => s.Planets).WillCascadeOnDelete(false);
            this.HasRequired(p => p.SolarSystem).WithMany(ss => ss.Plantes).WillCascadeOnDelete(false);
            this.HasMany(p => p.People).WithRequired(p => p.HomePlanet).WillCascadeOnDelete(false);
        }
    }
}
