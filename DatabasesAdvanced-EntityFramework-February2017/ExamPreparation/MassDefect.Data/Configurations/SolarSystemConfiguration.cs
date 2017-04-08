namespace MassDefect.Data.Configurations
{
    using MassDefect.Models;
    using System.Data.Entity.ModelConfiguration;

    class SolarSystemConfiguration : EntityTypeConfiguration<SolarSystem>
    {
        public SolarSystemConfiguration()
        {
            // Set primary key.
            this.HasKey(ss => ss.Id);

            // Set required fields.
            this.Property(ss => ss.Name).IsRequired();
        }
    }
}
