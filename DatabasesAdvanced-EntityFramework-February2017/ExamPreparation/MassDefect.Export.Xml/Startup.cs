namespace MassDefect.Export.Xml
{
    using MassDefect.Data;
    using System.Linq;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            var context = new MassDefectDbContext();

            var anomaliesForExport = context.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    otiginPlanet = a.OriginPlanet.Name,
                    teeportPlanet = a.TeleportPlanet.Name,
                    victims = a.Victims.Select(v => new
                    {
                        name = v.Name
                    })
                })
                .OrderBy(a => a.id)
                .ToList();

            var anomaliesNode = new XElement("anomalies");

            foreach (var anomaly in anomaliesForExport)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", anomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", anomaly.otiginPlanet));
                anomalyNode.Add(new XAttribute("teleport-planet", anomaly.teeportPlanet));

                var victimsNode = new XElement("victims");

                foreach (var victim in anomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim.name));

                    anomalyNode.Add(victimNode);
                }

                anomaliesNode.Add(anomalyNode);
            }

            var xmlDoc = new XDocument();
            xmlDoc.Add(anomaliesNode);

            xmlDoc.Save("../../anomalies.xml");
            System.Console.WriteLine(xmlDoc);          
        }
    }
}
