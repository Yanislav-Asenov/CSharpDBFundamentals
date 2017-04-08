namespace TeamBuilder.Data.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;
    using TeamBuilder.Models;

    class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            // Set primary key.
            this.HasKey(t => t.Id);

            // Set fields as required.
            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Acronym).IsRequired();

            // Set fields length.
            this.Property(t => t.Name).HasMaxLength(25);
            this.Property(t => t.Description).HasMaxLength(32);
            this.Property(t => t.Acronym).IsFixedLength().HasMaxLength(3);

            // Set fields as unique.
            this.Property(t => t.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_Teams_Name", 1) { IsUnique = true }));
        }
    }
}
