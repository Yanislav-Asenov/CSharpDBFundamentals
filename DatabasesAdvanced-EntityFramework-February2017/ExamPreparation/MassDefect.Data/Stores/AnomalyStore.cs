using MassDefect.Data.Dtos;
using MassDefect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace MassDefect.Data.Stores
{
    public class AnomalyStore
    {
        public static void AddAnomalies(IEnumerable<AnomalyDto> anomalies)
        {
            using (var context = new MassDefectDbContext())
            {
                foreach (var anomaly in anomalies)
                {
                    if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var originPlanet = PlanetStore.GetByName(context, anomaly.OriginPlanet);

                    if (originPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var teleportPlanet = PlanetStore.GetByName(context, anomaly.TeleportPlanet);

                    if (teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    context.Anomalies.Add(new Anomaly()
                    {
                        OriginPlanetId = originPlanet.Id,
                        TeleportPlanetId = teleportPlanet.Id
                    });

                    Console.WriteLine($"Successfully imported Anomaly.");
                }

                context.SaveChanges();
            }
        }

        public static void AddAnomalyVictims(IEnumerable<AnomalyVictimDto> anomalyVictims)
        {
            using (var context = new MassDefectDbContext())
            {
                foreach (var anomalyVictim in anomalyVictims)
                {
                    if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomalyEntity = GetById(context, (int) anomalyVictim.Id);

                    if (anomalyEntity == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var personEntity = PersonStore.GetByName(context, anomalyVictim.Person);

                    if (personEntity == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    anomalyEntity.Victims.Add(personEntity);
                    Console.WriteLine($"Successfully imported anomaly.");
                }

                context.SaveChanges();
            }
        }

        public static void AddAnomaliesAndVictimsFromXml(IEnumerable<XElement> anomalies)
        {
            using (var context = new MassDefectDbContext())
            {
                foreach (var anomaly in anomalies)
                {
                    var originPlanetName = anomaly.Attribute("origin-planet")?.Value;
                    var teleportPlanetName = anomaly.Attribute("teleport-planet")?.Value;

                    if (originPlanetName == null || teleportPlanetName == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var originPlanetEntity = PlanetStore.GetByName(context, originPlanetName);

                    if (originPlanetEntity == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var teleportPlanetEntity = PlanetStore.GetByName(context, teleportPlanetName);

                    if (teleportPlanetEntity == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var anomalyEntity = new Anomaly()
                    {
                        OriginPlanetId = originPlanetEntity.Id,
                        TeleportPlanetId = teleportPlanetEntity.Id
                    };

                    var victims = anomaly.XPathSelectElements("victims/victim");

                    foreach (var victim in victims)
                    {
                        var victimName = victim.Attribute("name")?.Value;

                        if (victimName == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                            continue;
                        }

                        var victimEntity = PersonStore.GetByName(context, victimName);

                        if (victimEntity == null)
                        {
                            Console.WriteLine("Error: Invalid data.");
                            continue;
                        }

                        anomalyEntity.Victims.Add(victimEntity);
                    }

                    context.Anomalies.Add(anomalyEntity);
                    Console.WriteLine($"Successfully imported anomaly with victims.");
                }

                context.SaveChanges();
            }
        }

        public static Anomaly GetById(MassDefectDbContext context, int id)
        {
            return context.Anomalies.Find(id);
        }
    }
}
