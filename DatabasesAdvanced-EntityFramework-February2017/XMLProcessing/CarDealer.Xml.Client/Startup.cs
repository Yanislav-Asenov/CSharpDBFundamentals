namespace CarDealer.Xml.Client
{
    using CarDealer.Xml.Data;
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            var context = new CarDealerXmlDbContext();

            #region 01. Cars
            //Cars(context);
            #endregion
            #region 02. Cars From Make Ferrari
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

        private static void SalesWithAppliedDiscount(CarDealerXmlDbContext context)
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

            var xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("sales",
                sales.Select(s => new XElement("sale",
                    new XElement("car",
                        new XAttribute("make", s.Car.Make),
                        new XAttribute("model", s.Car.Model),
                        new XAttribute("travelled-distance", s.Car.TravelledDistance)),
                    new XElement("customer-name", s.CustomerName),
                    new XElement("discount", s.Discount),
                    new XElement("price", s.Price),
                    new XElement("price-with-discount", s.PriceWithDiscount)
                    )
                )
            ));

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/sales-discounts.xml");
        }

        private static void TotalSalesByCustomer(CarDealerXmlDbContext context)
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

            var xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("customers",
                customers.Select(c => new XElement("customer",
                    new XAttribute("full-name", c.FullName),
                    new XAttribute("bought-cars", c.BoughtCars),
                    new XAttribute("spent-money", c.SpentMoney)
                    )
                )
            ));

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/customers-total-sales.xml");
        }

        private static void CarsWithTheirListOfParts(CarDealerXmlDbContext context)
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

            var xmlDoc = new XDocument();
            xmlDoc.Add(new XElement("cars",
                cars.Select(c => new XElement("car",
                    new XAttribute("make", c.Car.Make),
                    new XAttribute("model", c.Car.Model),
                    new XAttribute("travelled-distance", c.Car.TravelledDistance),
                    new XElement("parts",
                        c.Parts.Select(p =>
                            new XElement("part",
                                new XAttribute("name", p.Name),
                                new XAttribute("price", p.Price)
                            )
                        )
                    ))
                )
            ));

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/cars-and-parts.xml");
        }

        private static void LocalSuppliers(CarDealerXmlDbContext context)
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

            var xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("suppliers",
                suppliers.Select(s => 
                    new XElement("supplier",
                        new XAttribute("id", s.Id),
                        new XAttribute("name", s.Name),
                        new XAttribute("parts-count", s.PartsCount)
                ))
            ));

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/local-suppliers.xml");
        }

        private static void CarsFromMakeFerrari(CarDealerXmlDbContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Ferrari")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            var xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("cars",
                cars.Select(c => new XElement("car",
                    new XAttribute("id", c.Id),
                    new XAttribute("model", c.Model),
                    new XAttribute("travelled-distance", c.TravelledDistance))
                ))
            );

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/ferrari-cars.xml");
        }

        private static void Cars(CarDealerXmlDbContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .OrderBy(c => c.Model)
                .ToList();

            var xmlDoc = new XDocument();
            xmlDoc.Add(new XElement("cars",
                cars.Select(c => new XElement("car",
                    new XElement("make", c.Make),
                    new XElement("model", c.Model),
                    new XElement("travelled-distance", c.TravelledDistance))
                ))
            );

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/cars.xml");
        }
    }
}
