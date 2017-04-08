namespace TeamBuilder.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TeamBuilderDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeamBuilder.Data.TeamBuilderDbContext context)
        {
        }
    }
}
