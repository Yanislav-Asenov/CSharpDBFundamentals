namespace BookmakerSystem.Model
{
    using System.Collections.Generic;

    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public CompetitionType Type { get; set; }
        public ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
