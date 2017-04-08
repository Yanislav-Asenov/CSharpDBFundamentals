namespace TeamBuilder.Data.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using TeamBuilder.Models;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            // Set primary key.
            this.HasKey(u => u.Id);

            // Set fields as required.
            this.Property(u => u.Username).IsRequired();

            // Set relations
            this.HasMany(u => u.CreatedTeams).WithRequired(t => t.Creator).WillCascadeOnDelete(false);
            this.HasMany(u => u.CreatedEvents).WithRequired(e => e.Creator).WillCascadeOnDelete(false);
            this.HasMany(u => u.RecievedInvitations).WithRequired(i => i.InvitedUser).WillCascadeOnDelete(false);
            this.HasMany(u => u.Teams)
                .WithMany(t => t.Members)
                .Map(ut =>
                    {
                        ut.MapLeftKey("UserId");
                        ut.MapRightKey("TeamId");
                        ut.ToTable("UserTeams");
                    });

            // Set fields length.
            this.Property(p => p.Username).HasMaxLength(3).HasMaxLength(25);
            this.Property(p => p.FirstName).HasMaxLength(25);
            this.Property(p => p.LastName).HasMaxLength(25);
            this.Property(p => p.Password).HasMaxLength(6).HasMaxLength(30);

            // Set fields as unique.
            this.Property(u => u.Username).HasColumnAnnotation("IX_Users_Username", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));
        }
    }
}