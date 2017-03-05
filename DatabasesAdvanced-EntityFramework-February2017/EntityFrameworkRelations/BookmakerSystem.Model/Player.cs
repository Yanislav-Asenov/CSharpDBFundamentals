namespace BookmakerSystem.Model
{
    using System.Collections.Generic;

    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SquadNumber { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public bool IsCurrentlyInjured { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
        public ICollection<PlayerStatistic> PlayerStatistics { get; set; } = new HashSet<PlayerStatistic>();
    }
}
