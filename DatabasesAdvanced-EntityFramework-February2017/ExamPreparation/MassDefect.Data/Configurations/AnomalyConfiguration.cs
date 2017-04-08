namespace MassDefect.Data.Configurations
{
    using MassDefect.Models;
    using System.Data.Entity.ModelConfiguration;

    public class AnomalyConfiguration : EntityTypeConfiguration<Anomaly>
    {
        public AnomalyConfiguration()
        {
            // Set primary key.
            this.HasKey(a => a.Id);

            // Set relationships.
            this.HasRequired(a => a.OriginPlanet).WithMany(p => p.OriginAnomalies).WillCascadeOnDelete(false);
            this.HasRequired(a => a.TeleportPlanet).WithMany(p => p.TeleportAnomalies).WillCascadeOnDelete(false);
            this.HasMany(a => a.Victims)
                .WithMany(v => v.Anomalies)
                .Map(av =>
                {
                    av.MapLeftKey("AnomalyId");
                    av.MapRightKey("PersonId");
                    av.ToTable("AnomalyVictims");
                });
        }
    }
}
