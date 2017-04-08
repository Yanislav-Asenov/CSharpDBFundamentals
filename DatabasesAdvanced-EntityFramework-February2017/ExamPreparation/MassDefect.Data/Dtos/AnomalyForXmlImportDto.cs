namespace MassDefect.Data.Dtos
{
    using System.Collections.Generic;

    public class AnomalyForXmlImportDto
    {
        public string OriginPlanet { get; set; }
        public string TeleportPlanet { get; set; }
        public ICollection<PersonDto> Victims { get; set; } = new HashSet<PersonDto>();
    }
}
