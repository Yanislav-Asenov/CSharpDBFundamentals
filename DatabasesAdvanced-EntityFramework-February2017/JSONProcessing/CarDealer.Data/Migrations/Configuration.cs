namespace CarDealer.Data.Migrations
{
    using CarDealer.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealer.Data.CarDealerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CarDealer.Data.CarDealerDbContext context)
        {
            if (!context.Suppliers.Any())
            {
                ImportSuppliers(context);
            }

            if (!context.Parts.Any())
            {
                ImportParts(context);
            }

            if (!context.Cars.Any())
            {
                ImportCars(context);
            }

            if (!context.Customers.Any())
            {
                ImportCustomers(context);
            }

            if (!context.Sales.Any())
            {
                ImportSales(context);
            }
        }

        private void ImportSales(CarDealerDbContext context)
        {
            int[] availableDiscounts = new int[] { 0, 5, 10, 15, 20, 30, 40, 50 };
            var customers = context.Customers.ToList();
            var cars = context.Cars.ToList();

            var salesToInsert = new List<Sale>();

            var rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var randomDiscountIndex = rnd.Next(0, availableDiscounts.Length);
                var randomCarIndex = rnd.Next(0, cars.Count);
                var randomCustomerIndex = rnd.Next(0, customers.Count);

                salesToInsert.Add(new Sale()
                {
                    Car = cars[randomCarIndex],
                    Customer = customers[randomCustomerIndex],
                    Discount = availableDiscounts[randomDiscountIndex]
                });
            }

            context.Sales.AddRange(salesToInsert);
            context.SaveChanges();
        }

        private void ImportCustomers(CarDealerDbContext context)
        {
            string customersAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\customers.json"))
            {
                customersAsJsonString = reader.ReadToEnd();
            }

            var customersToInsert = JsonConvert.DeserializeObject<List<Customer>>(customersAsJsonString);

            context.Customers.AddRange(customersToInsert);
            context.SaveChanges();
        }

        private void ImportCars(CarDealerDbContext context)
        {
            string carsAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\cars.json"))
            {
                carsAsJsonString = reader.ReadToEnd();
            }

            var carsToInsert = JsonConvert.DeserializeObject<List<Car>>(carsAsJsonString);
            var parts = context.Parts.ToList();
            var rnd = new Random();

            foreach (var car in carsToInsert)
            {
                var numberOfCarsToInsert = rnd.Next(10, 21);

                for (var i = 0; i < numberOfCarsToInsert; i++)
                {
                    var randomPartIndex = rnd.Next(0, parts.Count);

                    car.Parts.Add(parts[randomPartIndex]);
                }
            }

            context.Cars.AddRange(carsToInsert);
            context.SaveChanges();
        }

        private void ImportParts(CarDealerDbContext context)
        {
            string partsAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\parts.json"))
            {
                partsAsJsonString = reader.ReadToEnd();
            }

            var partsToInsert = JsonConvert.DeserializeObject<List<Part>>(partsAsJsonString);
            var suppliers = context.Suppliers.ToList();
            var rnd = new Random();

            foreach (var part in partsToInsert)
            {
                var randomSupplier = suppliers[rnd.Next(0, suppliers.Count)];
                part.Supplier = randomSupplier;
                part.SupplierId = randomSupplier.Id;
            }

            context.Parts.AddRange(partsToInsert);
            context.SaveChanges();
        }

        private void ImportSuppliers(CarDealerDbContext context)
        {
            string suppliersAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\suppliers.json"))
            {
                suppliersAsJsonString = reader.ReadToEnd();
            }

            var suppliersToInsert = JsonConvert.DeserializeObject<List<Supplier>>(suppliersAsJsonString);

            context.Suppliers.AddRange(suppliersToInsert);
            context.SaveChanges();
        }
    }
}
