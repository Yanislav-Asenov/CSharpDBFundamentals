namespace MassDefect.Data.Stores
{
    using System;
    using System.Collections.Generic;
    using MassDefect.Data.Dtos;
    using MassDefect.Models;
    using System.Linq;

    public class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDto> planets)
        {
            using (var context = new MassDefectDbContext())
            {
                foreach (var planet in planets)
                {
                    if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var solarSystem = SolarSystemStore.GetByName(context, planet.SolarSystem);

                    if (solarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var sun = StarStore.GetByName(context, planet.Sun);

                    context.Plantes.Add(new Planet()
                    {
                        Name = planet.Name,
                        SolarSystemId = solarSystem.Id,
                        SunId = sun.Id
                    });

                    Console.WriteLine($"Successfully imported Planet {planet.Name}.");
                }

                context.SaveChanges();
            }
        }

        internal static Planet GetByName(MassDefectDbContext context, string planetName)
        {
            return context.Plantes.FirstOrDefault(p => p.Name == planetName);
        }
    }
}
