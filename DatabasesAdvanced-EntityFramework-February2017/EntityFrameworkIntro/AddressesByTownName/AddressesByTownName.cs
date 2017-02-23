namespace AddressesByTownName
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class AddressesByTownName
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var addresses = dbContext.Addresses
                .Select(a => new
                {
                    AddressText = a.AddressText,
                    TownName = a.Town.Name,
                    NumberOfEmployees = a.Employees.Count
                })
                .OrderByDescending(a => a.NumberOfEmployees)
                .ThenBy(a => a.TownName)
                .Take(10)
                .ToList();

            StringBuilder content = new StringBuilder();
            foreach (var a in addresses)
            {
                content.AppendLine($"{a.AddressText}, {a.TownName} - {a.NumberOfEmployees} employees");
            }
            Console.Write(content);
        }
    }
}
