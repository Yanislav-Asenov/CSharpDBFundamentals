namespace MassDefect.Data.Stores
{
    using MassDefect.Data.Dtos;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PersonStore
    {
        public static void AddPeople(IEnumerable<PersonDto> people)
        {
            using (var context = new MassDefectDbContext()) 
            {
                foreach (var person in people)
                {
                    if (person.Name == null || person.HomePlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var homePlanet = PlanetStore.GetByName(context, person.HomePlanet);

                    if (homePlanet == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    context.People.Add(new Person()
                    {
                        Name = person.Name,
                        HomePlanetId = homePlanet.Id
                    });

                    Console.WriteLine($"Successfully imported Person {person.Name}.");
                }

                context.SaveChanges();
            }
        }

        public static Person GetByName(MassDefectDbContext context, string personName)
        {
            return context.People.FirstOrDefault(p => p.Name == personName);
        }
    }
}
