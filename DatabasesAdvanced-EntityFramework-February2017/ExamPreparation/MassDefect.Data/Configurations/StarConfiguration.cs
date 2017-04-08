namespace MassDefect.Data.Configurations
{
    using MassDefect.Models;
    using System.Data.Entity.ModelConfiguration;

    public class StarConfiguration : EntityTypeConfiguration<Star>
    {
        public StarConfiguration()
        {
            // Set primary key.
            this.HasKey(s => s.Id);

            // Set required fields.
            this.Property(s => s.Name).IsRequired();

            // Set relationships.
            this.HasRequired(s => s.SolarSystem).WithMany(ss => ss.Stars);
        }
    }
}
