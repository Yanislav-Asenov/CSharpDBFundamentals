namespace BookmakerSystem.Data
{
    using Model;
    using System.Data.Entity;

    public class BookmakerSystemDbContext : DbContext
    {
        public BookmakerSystemDbContext()
            : base("name=BookmakerSystemDbContext")
        {
        }

        public IDbSet<Color> Colors { get; set; }
        public IDbSet<Continent> Continents { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Town> Towns { get; set; }
        public IDbSet<Player> Players { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<Game> Games { get; set; }
        public IDbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public IDbSet<Round> Rounds { get; set; }
        public IDbSet<Competition> Competitions { get; set; }
        public IDbSet<CompetitionType> CompetitionTypes { get; set; }
        public IDbSet<Bet> Bets { get; set; }
        public IDbSet<BetGame> BetGames { get; set; }
        public IDbSet<ResultPrediction> ResultPredictions { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasRequired(team => team.Town)
                .WithMany(town => town.Teams);

            modelBuilder.Entity<Team>()
                .HasRequired(t => t.PrimaryKitColor)
                .WithRequiredPrincipal(c => c.Team);

            modelBuilder.Entity<Team>()
                .HasRequired(t => t.SecondaryKitColor)
                .WithRequiredPrincipal(c => c.Team);

            modelBuilder.Entity<Town>()
                .HasRequired(town => town.Country)
                .WithMany(country => country.Towns);

            modelBuilder.Entity<Player>()
                .HasRequired(p => p.Position)
                .WithMany(position => position.Players);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Games)
                .WithMany(g => g.Players);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.PlayerStatistics)
                .WithRequired(ps => ps.Player);

            modelBuilder.Entity<Game>()
                .HasMany(g => g.PlayerStatistics)
                .WithRequired(ps => ps.Game);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.Round)
                .WithMany(r => r.Games);

            modelBuilder.Entity<Game>()
               .HasRequired(g => g.Competition)
               .WithMany(c => c.Games);


            modelBuilder.Entity<Game>()
               .HasMany(g => g.Bets)
               .WithMany(b => b.Games);

            modelBuilder.Entity<Game>()
               .HasMany(g => g.BetGames)
               .WithRequired(bg => bg.Game);

            modelBuilder.Entity<Bet>()
              .HasMany(b => b.BetGames)
              .WithRequired(bg => bg.Bet);

            modelBuilder.Entity<Bet>()
                .HasRequired(b => b.User)
                .WithMany(u => u.Bets);

            modelBuilder.Entity<BetGame>()
                .HasRequired(bg => bg.ResultPrediction)
                .WithRequiredPrincipal(rp => rp.BetGame);

            modelBuilder.Entity<PlayerStatistic>()
                .HasKey(ps => new { ps.GameId, ps.PlayerId });

            modelBuilder.Entity<Competition>()
                .HasRequired(c => c.Type)
                .WithMany(t => t.Competitions);

            base.OnModelCreating(modelBuilder);
        }
    }
}
