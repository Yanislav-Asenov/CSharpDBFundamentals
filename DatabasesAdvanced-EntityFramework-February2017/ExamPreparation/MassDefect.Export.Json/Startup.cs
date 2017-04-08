namespace MassDefect.Export.Json
{
    using MassDefect.Data;
    using System.Linq;
    using Newtonsoft.Json;
    using System.IO;

    public class Startup
    {
        public static void Main()
        {
            var context = new MassDefectDbContext();

            //ExportPlantesWhichAreNotAnomalyOrigins(context);
            //PeopleWhichHaveNotBeenVictims(context);
            //AnomaliesWhichAffectedTheMostPeople(context);
        }

        private static void ExportPlantesWhichAreNotAnomalyOrigins(MassDefectDbContext context)
        {
            var planetsForExport = context.Plantes
                .Where(p => !p.OriginAnomalies.Any())
                .Select(p => new
                {
                    name = p.Name
                });

            var plantesAsJson = JsonConvert.SerializeObject(planetsForExport, Formatting.Indented);
            File.WriteAllText("../../planets.json", plantesAsJson);
        }

        public static void PeopleWhichHaveNotBeenVictims(MassDefectDbContext context)
        {
            var peopleForExport = context.People
                .Where(p => !p.Anomalies.Any())
                .Select(p => new
                {
                    name = p.Name,
                    homePlanet = new
                    {
                        name = p.HomePlanet.Name
                    }
                });

            var peopleAsJson = JsonConvert.SerializeObject(peopleForExport, Formatting.Indented);

            File.WriteAllText("../../people.json", peopleAsJson);
        }

        public static void AnomaliesWhichAffectedTheMostPeople(MassDefectDbContext context)
        {
            var anomalyForExport = context.Anomalies
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = new
                    {
                        name = a.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = a.TeleportPlanet.Name
                    },
                    victimsCount = a.Victims.Count
                })
                .OrderByDescending(a => a.victimsCount)
                .Take(1);


            var anomaliesAsJson = JsonConvert.SerializeObject(anomalyForExport, Formatting.Indented);

            File.WriteAllText("../../anomaly.json", anomaliesAsJson);
        }
    }
}
