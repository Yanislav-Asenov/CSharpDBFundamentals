namespace MassDefect.Models
{
    using System.Collections.Generic;

    public class Planet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SunId { get; set; }
         
        public virtual Star Sun { get; set; }

        public int SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> People { get; set; } = new HashSet<Person>();

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; } = new HashSet<Anomaly>();

        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; } = new HashSet<Anomaly>();
    }
}
