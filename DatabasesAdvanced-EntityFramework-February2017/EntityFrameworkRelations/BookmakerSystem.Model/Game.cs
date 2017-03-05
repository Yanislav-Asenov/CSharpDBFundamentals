namespace BookmakerSystem.Model
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        public int Id { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; }
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public DateTime DateTime { get; set; }
        public decimal HomeTeamWinBetRate { get; set; }
        public decimal AwayTeamWinBetRate { get; set; }
        public decimal DrawBetRate { get; set; }
        public int RoundId { get; set; }
        public Round Round { get; set; }
        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }
        public ICollection<BetGame> BetGames { get; set; } = new HashSet<BetGame>();
        public ICollection<Bet> Bets { get; set; } = new HashSet<Bet>();
        public ICollection<Player> Players { get; set; } = new HashSet<Player>();
        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();
    }
}
