namespace BookmakerSystem.Model
{
    using System;
    using System.Collections.Generic;

    public class Bet
    {
        public int Id { get; set; }
        public decimal BetMoney { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
        public ICollection<BetGame> BetGames { get; set; } = new HashSet<BetGame>();
    }
}
