namespace CarDealer.Client
{
    using CarDealer.Data;
    using System.Linq;
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using System.Data.Entity;

    public class Startup
    {
        static void Main()
        {
            var context = new CarDealerDbContext();

            #region 01. Ordered Customers
            //OrderedCustomers(context);
            #endregion
            #region 02. Cars From Make Toyota
            //CarsFromMakeToyota(context);
            #endregion
            #region 03. Local Suppliers
            //LocalSuppliers(context);
            #endregion
            #region 04. Cars With Their List Of Parts
            //CarsWithTheirListOfParts(context);
            #endregion
            #region 05. Total Sales By Customer
            //TotalSalesByCustomer(context);
            #endregion
            #region 06. Sales With Applied Discount
            //SalesWithAppliedDiscount(context);
            #endregion
        }

        private static void SalesWithAppliedDiscount(CarDealerDbContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    Car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car.Parts.Sum(p => p.Price),
                    PriceWithDiscount = (s.Car.Parts.Sum(p => p.Price) * s.Discount) / 100
                })
                .ToList();

            var salesAsJsonString = JsonConvert.SerializeObject(sales, Formatting.Indented);

            WriteToFile("customers-total-sales.json", salesAsJsonString);

            Console.WriteLine(salesAsJsonString);
        }

        private static void TotalSalesByCustomer(CarDealerDbContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() > 0)
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            var customersAsJsonString = JsonConvert.SerializeObject(customers, Formatting.Indented);

            WriteToFile("customers-total-sales.json", customersAsJsonString);

            Console.WriteLine(customersAsJsonString);
        }

        private static void CarsWithTheirListOfParts(CarDealerDbContext context)
        {
            var cars = context.Cars
                .Include(c => c.Parts)
                .Select(c => new
                {
                    Car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    Parts = c.Parts.Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                })
                .ToList();

            var carsAsJsonString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            WriteToFile("cars-and-parts.json", carsAsJsonString);

            Console.WriteLine(carsAsJsonString);
        }

        private static void LocalSuppliers(CarDealerDbContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();

            var suppliersAsJsonString = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            WriteToFile("local-suppliers.json", suppliersAsJsonString);

            Console.WriteLine(suppliersAsJsonString);
        }

        private static void CarsFromMakeToyota(CarDealerDbContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var carsAsJsonString = JsonConvert.SerializeObject(cars, Formatting.Indented);

            WriteToFile("toyota-cars.json", carsAsJsonString);

            Console.WriteLine(carsAsJsonString);
        }

        private static void OrderedCustomers(CarDealerDbContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver == false)
                .ToList();

            var customersAsJsonString = JsonConvert.SerializeObject(customers, Formatting.Indented);

            WriteToFile("ordered-customers.json", customersAsJsonString);

            Console.WriteLine(customersAsJsonString);
        }

        private static void WriteToFile(string fileName, string fileContent)
        {
            string exportsFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Exports";
            string filePath = $"{exportsFolder}\\{fileName}";

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(fileContent);
            }
        }
    }
}
