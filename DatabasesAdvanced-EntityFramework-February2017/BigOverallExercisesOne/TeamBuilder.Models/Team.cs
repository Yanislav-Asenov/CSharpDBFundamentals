namespace TeamBuilder.Models
{
    using System.Collections.Generic;

    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; } = new HashSet<Invitation>();

        public virtual ICollection<User> Members { get; set; } = new HashSet<User>();

        public virtual ICollection<Event> ParticipatedEvents { get; set; } = new HashSet<Event>();
    }
}
