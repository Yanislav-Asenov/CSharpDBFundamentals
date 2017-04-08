namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HomePlanetId { get; set; }

        public virtual Planet HomePlanet { get; set; }

        public virtual ICollection<Anomaly> Anomalies { get; set; } = new HashSet<Anomaly>();
    }
}
