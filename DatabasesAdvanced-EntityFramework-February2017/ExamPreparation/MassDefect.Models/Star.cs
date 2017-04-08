namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Star
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Planet> Planets { get; set; } = new HashSet<Planet>();
    }
}
