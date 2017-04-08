namespace TeamBuilder.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using TeamBuilder.Models;

    class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            // Set primary key.
            this.HasKey(e => e.Id);

            // Set properties length
            this.Property(e => e.Name).HasMaxLength(25);
            this.Property(e => e.Description).HasMaxLength(250);

            // Set relations
            this.HasRequired(e => e.Creator).WithMany(c => c.CreatedEvents);
            this.HasMany(e => e.ParticipatingTeams)
                .WithMany(t => t.ParticipatedEvents)
                .Map(et =>
                    {
                        et.MapLeftKey("EventId");
                        et.MapRightKey("TeamId");
                        et.ToTable("EventTeams");
                    });

            // Configure property types
            this.Property(e => e.StartDate).HasColumnType("datetime2");
            this.Property(e => e.EndDate).HasColumnType("datetime2");

            // Set properties unicode
            this.Property(e => e.Name).IsUnicode(true);
            this.Property(e => e.Description).IsUnicode(true);
        }
    }
}
