namespace MassDefect.Data.Stores
{
    using MassDefect.Data.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class SolarSystemStore
    {
        public static void AddSolarSystems(IEnumerable<SolarSystemDto> solarSystems)
        {
            using (var context = new MassDefectDbContext())
            {

                foreach (var solarSystem in solarSystems)
                {
                    if (solarSystem.Name == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    context.SolarSystems.Add(new SolarSystem() { Name = solarSystem.Name });

                    Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}");
                }

                context.SaveChanges();
            }
        }

        public static SolarSystem GetByName(MassDefectDbContext context, string solarSystemName)
        {
            return context.SolarSystems.FirstOrDefault(ss => ss.Name == solarSystemName);
        }
    }
}
