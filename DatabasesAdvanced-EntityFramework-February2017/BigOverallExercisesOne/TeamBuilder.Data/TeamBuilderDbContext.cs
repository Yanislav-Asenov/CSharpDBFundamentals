namespace TeamBuilder.Data
{
    using System.Data.Entity;
    using TeamBuilder.Data.Configurations;
    using TeamBuilder.Data.Migrations;
    using TeamBuilder.Models;

    public class TeamBuilderDbContext : DbContext
    {
        public TeamBuilderDbContext()
            : base("name=TeamBuilderDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TeamBuilderDbContext, Configuration>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new TeamConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
