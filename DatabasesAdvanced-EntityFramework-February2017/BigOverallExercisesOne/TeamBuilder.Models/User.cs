namespace TeamBuilder.Models
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Invitation> RecievedInvitations { get; set; } = new HashSet<Invitation>();

        public virtual ICollection<Team> CreatedTeams { get; set; } = new HashSet<Team>();

        public virtual ICollection<Event> CreatedEvents { get; set; } = new HashSet<Event>();

        public virtual ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
