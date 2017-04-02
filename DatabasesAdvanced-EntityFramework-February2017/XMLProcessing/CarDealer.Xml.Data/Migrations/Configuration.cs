namespace CarDealer.Xml.Data.Migrations
{
    using CarDealer.Xml.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealerXmlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealerXmlDbContext context)
        {
            if (!context.Suppliers.Any())
            {
                ImportSuppliers();
            }

            if (!context.Parts.Any())
            {
                ImportParts();
            }

            if (!context.Cars.Any())
            {
                ImportCars();
            }

            if (!context.Customers.Any())
            {
                ImportCustomers();
            }

            if (!context.Sales.Any())
            {
                ImportSales();
            }
        }

        private void ImportSales()
        {
            CarDealerXmlDbContext context = new CarDealerXmlDbContext();

            int[] availableDiscounts = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };
            var customers = context.Customers.ToList();
            var cars = context.Cars.ToList();

            var salesToInsert = new List<Sale>();

            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                salesToInsert.Add(new Sale()
                {
                    Car = cars[rnd.Next(0, cars.Count)],
                    Customer = customers[rnd.Next(0, customers.Count - 1)],
                    Discount = availableDiscounts[rnd.Next(0, availableDiscounts.Length)]
                });
            }

            context.Sales.AddRange(salesToInsert);
            context.SaveChanges();
        }

        private void ImportCustomers()
        {
            CarDealerXmlDbContext context = new CarDealerXmlDbContext();

            var usersXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/customers.xml");

            var customersToInsert = usersXmlDoc.Root
             .Elements()
             .Select(u => new Customer()
             {
                 Name = u.Attribute("name")?.Value,
                 BirthDate = DateTime.Parse(u.Element("birth-date")?.Value),
                 IsYoungDriver = bool.Parse(u.Element("is-young-driver")?.Value ?? "false")
             });

            context.Customers.AddRange(customersToInsert);
            context.SaveChanges();
        }

        private void ImportCars()
        {
            CarDealerXmlDbContext context = new CarDealerXmlDbContext();

            var usersXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/cars.xml");

            var carsToInsert = usersXmlDoc.Root
             .Elements()
             .Select(u => new Car()
             {
                 Make = u.Element("make")?.Value ?? string.Empty,
                 Model = u.Element("model")?.Value ?? string.Empty,
                 TravelledDistance = double.Parse(u.Element("travelled-distance")?.Value ?? "0.00")
             })
             .ToList();

            var parts = context.Parts.ToList();
            var rnd = new Random();

            foreach (var car in carsToInsert)
            {
                var numberOfCarsToInsert = rnd.Next(1, 5);

                for (var i = 0; i < numberOfCarsToInsert; i++)
                {
                    car.Parts.Add(parts[rnd.Next(0, parts.Count)]);
                }
            }

            context.Cars.AddRange(carsToInsert);
            context.SaveChanges();
        }

        private void ImportParts()
        {
            CarDealerXmlDbContext context = new CarDealerXmlDbContext();

            var usersXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/parts.xml");

            var partsToInsert = usersXmlDoc.Root
             .Elements()
             .Select(u => new Part()
             {
                 Name = u.Attribute("name").Value,
                 Price = decimal.Parse(u.Attribute("price").Value),
                 Quantity = int.Parse(u.Attribute("quantity").Value)
             })
             .ToList();

            var suppliers = context.Suppliers.ToList();
            var rnd = new Random();

            foreach (var part in partsToInsert)
            {
                part.SupplierId = suppliers[rnd.Next(0, suppliers.Count)].Id;
            }

            context.Parts.AddRange(partsToInsert);
            context.SaveChanges();
        }

        private void ImportSuppliers()
        {
            CarDealerXmlDbContext context = new CarDealerXmlDbContext();

            var usersXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/suppliers.xml");

            var usersToInsert = usersXmlDoc.Root
              .Elements()
              .Select(u => new Supplier()
              {
                  Name = u.Attribute("name").Value,
                  IsImporter = bool.Parse(u.Attribute("is-importer").Value)
              });

            context.Suppliers.AddRange(usersToInsert);
            context.SaveChanges();
        }
    }
}
