namespace ProductsShop.Data.Migrations
{
    using Newtonsoft.Json;
    using ProductsShop.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Threading.Tasks;
    using System;
    using System.Reflection;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductsShop.Data.ProductsShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProductsShop.Data.ProductsShopDbContext context)
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

        private void InsertProducts(ProductsShopDbContext context)
        {
            string productsAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\products.json"))
            {
                productsAsJsonString = reader.ReadToEnd();
            }

            var productsToInsert = JsonConvert.DeserializeObject<List<Product>>(productsAsJsonString);

            var users = context.Users.ToList();
            var categories = context.Categories.ToList();

            Random rnd = new Random();
            for (int i = 0; i < productsToInsert.Count - 15; i++)
            {
                var product = productsToInsert[i];
                int buyerIndex = rnd.Next(0, users.Count / 2);
                product.Buyer = users[buyerIndex];

                int sellerIndex = rnd.Next(users.Count / 2 + 1, users.Count - 1);
                product.Seller = users[sellerIndex];

                int categoryIndex = rnd.Next(0, categories.Count);
                product.Categories.Add(categories[categoryIndex]);
            }

            for (int i = productsToInsert.Count - 15; i < productsToInsert.Count; i++)
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

        private void InsertCategories(ProductsShopDbContext context)
        {
            string categoriesAsJsonString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\categories.json"))
            {
                categoriesAsJsonString = reader.ReadToEnd();
            }

            var categoriesToInsert = JsonConvert.DeserializeObject<IEnumerable<Category>>(categoriesAsJsonString);

            context.Categories.AddRange(categoriesToInsert);
            context.SaveChanges();
        }

        private void InsertUsers(ProductsShopDbContext context)
        {
            string usersJsonDataAsString = string.Empty;

            using (StreamReader reader = new StreamReader($"{AppDomain.CurrentDomain.BaseDirectory}\\Imports\\users.json"))
            {
                usersJsonDataAsString = reader.ReadToEnd();
            }

            var usersToInsert = JsonConvert.DeserializeObject<List<User>>(usersJsonDataAsString);

            context.Users.AddRange(usersToInsert);
            context.SaveChanges();
        }
    }
}
