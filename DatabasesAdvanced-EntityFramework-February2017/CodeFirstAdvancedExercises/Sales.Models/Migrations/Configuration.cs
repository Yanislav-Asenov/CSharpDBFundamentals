namespace Sales.Models.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Sales.Models.SalesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Sales.Models.SalesDbContext";
        }

        protected override void Seed(SalesDbContext context)
        {
            var salesToInsert = new List<Sale>()
            {
                new Sale()
                {
                    Product = new Product()
                    {
                        Name = "Product 1",
                        Price = 55.55m,
                        Quantity = 5
                    },
                    StoreLocation = new StoreLocation()
                    {
                        LoctionName = "Location Name 1"
                    },
                    Customer = new Customer()
                    {
                        FirstName = "Customer 1",
                        LastName = "Customer 1",
                        CreditCardNumber = "1234567890",
                        Email = "customer1@gmail.com",
                        //Age = 21
                    },
                    Date = DateTime.Now
                },
                new Sale()
                {
                    Product = new Product()
                    {
                        Name = "Product 2",
                        Price = 22.55m,
                        Quantity = 51
                    },
                    StoreLocation = new StoreLocation()
                    {
                        LoctionName = "Location Name 2"
                    },
                    Customer = new Customer()
                    {
                        FirstName = "Customer 2",
                        LastName = "Customer 2",
                        CreditCardNumber = "0987654321",
                        Email = "customer2@gmail.com",
                        //Age = 22
                    },
                    Date = DateTime.Now
                },
                new Sale()
                {
                    Product = new Product()
                    {
                        Name = "Product 3",
                        Price = 33.55m,
                        Quantity = 33
                    },
                    StoreLocation = new StoreLocation()
                    {
                        LoctionName = "Location Name 3"
                    },
                    Customer = new Customer()
                    {
                        FirstName = "Customer 3",
                        LastName = "Customer 3",
                        CreditCardNumber = "33333333333",
                        Email = "customer3@gmail.com",
                        //Age = 23
                    },
                    Date = DateTime.Now
                },
                new Sale()
                {
                    Product = new Product()
                    {
                        Name = "Product 4",
                        Price = 44.55m,
                        Quantity = 44
                    },
                    StoreLocation = new StoreLocation()
                    {
                        LoctionName = "Location Name 4"
                    },
                    Customer = new Customer()
                    {
                        FirstName = "Customer 4",
                        LastName = "Customer 4",
                        CreditCardNumber = "444444444444",
                        Email = "customer4@gmail.com",
                        //Age = 24
                    },
                    Date = DateTime.Now
                },
                new Sale()
                {
                    Product = new Product()
                    {
                        Name = "Product 5",
                        Price = 55.55m,
                        Quantity = 55
                    },
                    StoreLocation = new StoreLocation()
                    {
                        LoctionName = "Location Name 5"
                    },
                    Customer = new Customer()
                    {
                        FirstName = "Customer 5",
                        LastName = "Customer 5",
                        CreditCardNumber = "55555555123",
                        Email = "customer5@gmail.com",
                        //Age = 25
                    },
                    Date = DateTime.Now
                }
            };

            context.Sales.AddRange(salesToInsert);
            context.SaveChanges();
        }
    }
}
