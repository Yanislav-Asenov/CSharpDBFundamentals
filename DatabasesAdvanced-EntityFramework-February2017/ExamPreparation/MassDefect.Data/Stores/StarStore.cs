namespace MassDefect.Data.Stores
{
    using MassDefect.Data.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StarStore
    {
        public static void AddStars(IEnumerable<StarDto> stars)
        {
            using (var context = new MassDefectDbContext())
            {
                foreach (var star in stars)
                {
                    if (star.Name == null || star.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var solarSystem = SolarSystemStore.GetByName(context, star.SolarSystem);

                    if (solarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    solarSystem.Stars.Add(new Star() { Name = star.Name, SolarSystemId = solarSystem.Id });

                    Console.WriteLine($"Successfully imported Star {star.Name}.");
                }

                context.SaveChanges();
            }
        }

        public static Star GetByName(MassDefectDbContext context, string starName)
        {
            return context.Stars.FirstOrDefault(s => s.Name == starName);
        }
    }
}
