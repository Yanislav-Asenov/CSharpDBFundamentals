namespace ProductsShop.Xml.Data.Migrations
{
    using ProductsShop.Xml.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsShopXmlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProductsShopXmlDbContext context)
        {
            if (!context.Users.Any())
            {
                InsertUsers(context);
            }

            if (!context.Categories.Any())
            {
                InsertCategories(context);
            }

            if (!context.Products.Any())
            {
                InsertProducts(context);
            }
        }

        private void InsertProducts(ProductsShopXmlDbContext context)
        {
            var productsXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/products.xml");

            var productsToInsert = productsXmlDoc.Root
                .Elements()
                .Select(p => new Product()
                {
                    Name = p.Element("name")?.Value ?? "No Name",
                    Price = decimal.Parse(p.Element("price")?.Value ?? "0.00")
                })
                .ToList();

            var users = context.Users.ToList();
            var categories = context.Categories.ToList();

            Random rnd = new Random();
            for (int i = 0; i < productsToInsert.Count - 5; i++)
            {
                var product = productsToInsert[i];
                int buyerIndex = rnd.Next(0, users.Count / 2);
                product.Buyer = users[buyerIndex];

                int sellerIndex = rnd.Next(users.Count / 2 + 1, users.Count - 1);
                product.Seller = users[sellerIndex];

                int categoryIndex = rnd.Next(0, categories.Count);
                product.Categories.Add(categories[categoryIndex]);
            }

            for (int i = productsToInsert.Count - 5; i < productsToInsert.Count; i++)
            {
                var product = productsToInsert[i];
                product.Buyer = null;
                product.BuyerId = null;

                int sellerIndex = rnd.Next(0, users.Count - 1);
                product.Seller = users[sellerIndex];

                int categoryIndex = rnd.Next(0, categories.Count);
                product.Categories.Add(categories[categoryIndex]);
            }

            context.Products.AddRange(productsToInsert);
            context.SaveChanges();
        }

        private void InsertCategories(ProductsShopXmlDbContext context)
        {
            var categoriesXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/categories.xml");

            var categoriesToInsert = categoriesXmlDoc.Root
                .Elements()
                .Select(c => new Category()
                {
                    Name = c.Element("name")?.Value ?? "No Name"
                });

            context.Categories.AddRange(categoriesToInsert);
            context.SaveChanges();
        }

        private void InsertUsers(ProductsShopXmlDbContext context)
        {
            var usersXmlDoc = XDocument.Load($"{AppDomain.CurrentDomain.BaseDirectory}../../Imports/users.xml");

            var usersToInsert = usersXmlDoc.Root
              .Elements()
              .Select(u => new User()
              {
                  FirstName = u.Attribute("first-name")?.Value ?? string.Empty,
                  LastName = u.Attribute("last-name")?.Value ?? string.Empty,
                  Age = int.Parse(u.Attribute("age")?.Value ?? "0")
              });

            context.Users.AddRange(usersToInsert);
            context.SaveChanges();
        }
    }
}
