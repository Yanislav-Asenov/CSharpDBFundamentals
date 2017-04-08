namespace MassDefect.Import.Json
{
    using MassDefect.Data.Stores;
    using MassDefect.Data.Dtos;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;

    public static class JsonImport
    {
        private const string SolarSystemPath = "../../../datasets/solar-systems.json";
        private const string StarsPath = "../../../datasets/stars.json";
        private const string PlanetsPath = "../../../datasets/planets.json";
        private const string PeoplePath = "../../../datasets/persons.json";
        private const string AnomaliesPath = "../../../datasets/anomalies.json";
        private const string AnomalyVictimsPath = "../../../datasets/anomaly-victims.json";

        public static void ImportSolarSystems()
        {
            var json = File.ReadAllText(SolarSystemPath);
            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDto>>(json);
            SolarSystemStore.AddSolarSystems(solarSystems);
        }

        public static void ImportStars()
        {
            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDto>>(json);
            StarStore.AddStars(stars);
        }

        public static void ImportPlanets()
        {
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDto>>(json);
            PlanetStore.AddPlanets(planets);
        }

        public static void ImportPeople()
        {
            var json = File.ReadAllText(PeoplePath);
            var people = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(json);
            PersonStore.AddPeople(people);
        }

        public static void ImportAnomalies()
        {
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDto>>(json);
            AnomalyStore.AddAnomalies(anomalies);
        }

        public static void ImportAnomalyVictims()
        {
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimDto>>(json);
            AnomalyStore.AddAnomalyVictims(anomalies);
        }

    }
}
