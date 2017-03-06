namespace BookmakerSystem.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BetGames",
                c => new
                {
                    BetId = c.Int(nullable: false),
                    GameId = c.Int(nullable: false),
                    ResultPredictionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.BetId, t.GameId })
                .ForeignKey("dbo.Bets", t => t.BetId, cascadeDelete: false)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: false)
                .Index(t => t.BetId)
                .Index(t => t.GameId);

            CreateTable(
                "dbo.Bets",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    BetMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DateTime = c.DateTime(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Games",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HomeTeamId = c.Int(nullable: false),
                    AwayTeamId = c.Int(nullable: false),
                    HomeGoals = c.Int(nullable: false),
                    AwayGoals = c.Int(nullable: false),
                    DateTime = c.DateTime(nullable: false),
                    HomeTeamWinBetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    AwayTeamWinBetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DrawBetRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    RoundId = c.Int(nullable: false),
                    CompetitionId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Competitions", t => t.CompetitionId, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId, cascadeDelete: false)
                .ForeignKey("dbo.Rounds", t => t.RoundId, cascadeDelete: false)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.RoundId)
                .Index(t => t.CompetitionId);

            CreateTable(
                "dbo.Teams",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Logo = c.Binary(),
                    Initials = c.String(),
                    PrimaryKitColorId = c.Int(nullable: false),
                    SecondaryKitColorId = c.Int(nullable: false),
                    TownId = c.Int(nullable: false),
                    Budged = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colors", t => t.PrimaryKitColorId, cascadeDelete: false)
                .ForeignKey("dbo.Colors", t => t.SecondaryKitColorId, cascadeDelete: false)
                .ForeignKey("dbo.Towns", t => t.TownId, cascadeDelete: false)
                .Index(t => t.PrimaryKitColorId)
                .Index(t => t.SecondaryKitColorId)
                .Index(t => t.TownId);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    SquadNumber = c.Int(nullable: false),
                    TeamId = c.Int(nullable: false),
                    PositionId = c.Int(nullable: false),
                    IsCurrentlyInjured = c.Boolean(nullable: false),
                    Position_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .Index(t => t.TeamId)
                .Index(t => t.Position_Id);

            CreateTable(
                "dbo.PlayerStatistics",
                c => new
                {
                    GameId = c.Int(nullable: false),
                    PlayerId = c.Int(nullable: false),
                    ScoredGoals = c.Int(nullable: false),
                    PlayerAssists = c.Int(nullable: false),
                    PlayedMinutesDuringGame = c.Double(nullable: false),
                })
                .PrimaryKey(t => new { t.GameId, t.PlayerId })
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: false)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: false)
                .Index(t => t.GameId)
                .Index(t => t.PlayerId);

            CreateTable(
                "dbo.Positions",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Description = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Colors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Towns",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    CountryId = c.Int(nullable: false),
                    Country_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: false)
                .Index(t => t.Country_Id);

            CreateTable(
                "dbo.Countries",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(),
                    ContinentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Continents",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Competitions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    TypeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompetitionTypes", t => t.TypeId, cascadeDelete: false)
                .Index(t => t.TypeId);

            CreateTable(
                "dbo.CompetitionTypes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Rounds",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(),
                    Password = c.String(),
                    Email = c.String(),
                    FullName = c.String(),
                    Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ResultPredictions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Prediction = c.String(),
                    BetGameId = c.Int(nullable: false),
                    BetGame_BetId = c.Int(nullable: false),
                    BetGame_GameId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BetGames", t => new { t.BetGame_BetId, t.BetGame_GameId })
                .Index(t => new { t.BetGame_BetId, t.BetGame_GameId });

            CreateTable(
                "dbo.PlayerGames",
                c => new
                {
                    Player_Id = c.Int(nullable: false),
                    Game_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Player_Id, t.Game_Id })
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: false)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: false)
                .Index(t => t.Player_Id)
                .Index(t => t.Game_Id);

            CreateTable(
                "dbo.ContinentCountries",
                c => new
                {
                    Continent_Id = c.Int(nullable: false),
                    Country_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.Continent_Id, t.Country_Id })
                .ForeignKey("dbo.Continents", t => t.Continent_Id, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: false)
                .Index(t => t.Continent_Id)
                .Index(t => t.Country_Id);

            CreateTable(
                "dbo.GameBets",
                c => new
                {
                    Game_Id = c.Int(nullable: false),
                    Bet_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Game_Id, t.Bet_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: false)
                .ForeignKey("dbo.Bets", t => t.Bet_Id, cascadeDelete: false)
                .Index(t => t.Game_Id)
                .Index(t => t.Bet_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ResultPredictions", new[] { "BetGame_BetId", "BetGame_GameId" }, "dbo.BetGames");
            DropForeignKey("dbo.Bets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Games", "RoundId", "dbo.Rounds");
            DropForeignKey("dbo.PlayerStatistics", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Games", "CompetitionId", "dbo.Competitions");
            DropForeignKey("dbo.Competitions", "TypeId", "dbo.CompetitionTypes");
            DropForeignKey("dbo.GameBets", "Bet_Id", "dbo.Bets");
            DropForeignKey("dbo.GameBets", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.BetGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "AwayTeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "TownId", "dbo.Towns");
            DropForeignKey("dbo.Towns", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.ContinentCountries", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.ContinentCountries", "Continent_Id", "dbo.Continents");
            DropForeignKey("dbo.Teams", "SecondaryKitColorId", "dbo.Colors");
            DropForeignKey("dbo.Teams", "PrimaryKitColorId", "dbo.Colors");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Players", "Position_Id", "dbo.Positions");
            DropForeignKey("dbo.PlayerStatistics", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlayerGames", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.BetGames", "BetId", "dbo.Bets");
            DropIndex("dbo.GameBets", new[] { "Bet_Id" });
            DropIndex("dbo.GameBets", new[] { "Game_Id" });
            DropIndex("dbo.ContinentCountries", new[] { "Country_Id" });
            DropIndex("dbo.ContinentCountries", new[] { "Continent_Id" });
            DropIndex("dbo.PlayerGames", new[] { "Game_Id" });
            DropIndex("dbo.PlayerGames", new[] { "Player_Id" });
            DropIndex("dbo.ResultPredictions", new[] { "BetGame_BetId", "BetGame_GameId" });
            DropIndex("dbo.Competitions", new[] { "TypeId" });
            DropIndex("dbo.Towns", new[] { "Country_Id" });
            DropIndex("dbo.PlayerStatistics", new[] { "PlayerId" });
            DropIndex("dbo.PlayerStatistics", new[] { "GameId" });
            DropIndex("dbo.Players", new[] { "Position_Id" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.Teams", new[] { "TownId" });
            DropIndex("dbo.Teams", new[] { "SecondaryKitColorId" });
            DropIndex("dbo.Teams", new[] { "PrimaryKitColorId" });
            DropIndex("dbo.Games", new[] { "CompetitionId" });
            DropIndex("dbo.Games", new[] { "RoundId" });
            DropIndex("dbo.Games", new[] { "AwayTeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeamId" });
            DropIndex("dbo.Bets", new[] { "UserId" });
            DropIndex("dbo.BetGames", new[] { "GameId" });
            DropIndex("dbo.BetGames", new[] { "BetId" });
            DropTable("dbo.GameBets");
            DropTable("dbo.ContinentCountries");
            DropTable("dbo.PlayerGames");
            DropTable("dbo.ResultPredictions");
            DropTable("dbo.Users");
            DropTable("dbo.Rounds");
            DropTable("dbo.CompetitionTypes");
            DropTable("dbo.Competitions");
            DropTable("dbo.Continents");
            DropTable("dbo.Countries");
            DropTable("dbo.Towns");
            DropTable("dbo.Colors");
            DropTable("dbo.Positions");
            DropTable("dbo.PlayerStatistics");
            DropTable("dbo.Players");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
            DropTable("dbo.Bets");
            DropTable("dbo.BetGames");
        }
    }
}
