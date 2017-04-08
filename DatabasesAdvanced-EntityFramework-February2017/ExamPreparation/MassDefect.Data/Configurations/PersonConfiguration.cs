namespace MassDefect.Data.Configurations
{
    using MassDefect.Models;
    using System.Data.Entity.ModelConfiguration;

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            // Set primary key.
            this.HasKey(p => p.Id);

            // Set required fields.
            this.Property(p => p.Name).IsRequired();
        }
    }
}
