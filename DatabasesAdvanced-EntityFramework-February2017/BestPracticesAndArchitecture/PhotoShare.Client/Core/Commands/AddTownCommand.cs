namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;

    public class AddTownCommand : Command
    {
        // AddTown <townName> <countryName>
        public override string Execute(string[] data)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Towns.Any(t => t.Name == townName))
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }

                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return $"Town {townName} was added successfully!";
            }
        }
    }
}
