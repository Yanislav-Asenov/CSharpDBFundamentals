namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class SolarSystem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Star> Stars { get; set; } = new HashSet<Star>();

        public virtual ICollection<Planet> Plantes { get; set; } = new HashSet<Planet>();
    }
}
