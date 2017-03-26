namespace ProductsShop.Client
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Newtonsoft.Json;
    using ProductsShop.Client.Dtos;
    using ProductsShop.Data;
    using ProductsShop.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Program
    {
        static void Main()
        {
            ConfigureAutoMapper();

            var context = new ProductsShopDbContext();

            #region 01. Products In Range
            //ProductsInRange(context);
            #endregion
            #region 02. Successfully Sold Products
            //SuccessfullySoldProducts(context);
            #endregion
            #region 03. Categories By Products
            //CategoriesByProducts(context);
            #endregion
            #region 04. Users And Products
            //UsersAndProducts(context);
            #endregion

        }

        private static void UsersAndProducts(ProductsShopDbContext context)
        {
            var usersAndProducts = new
            {
                UsersCount = context.Users.Count(),
                Users = context.Users
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        SoldProducts = new
                        {
                            Count = u.ProductsSold.Count(),
                            Products = u.ProductsSold.Select(p => new
                            {
                                p.Name,
                                p.Price
                            })
                        }
                    })
            };

            var usersAndProductsAsJson = JsonConvert.SerializeObject(usersAndProducts);

            WriteToFile("users-and-products.json", usersAndProductsAsJson);
        }

        private static void CategoriesByProducts(ProductsShopDbContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    ProductsCount = c.Products.Count(),
                    AveragePrice = c.Products.Average(p => p.Price),
                    TotalRevenue = c.Products.Sum(p => p.Price)
                })
                .OrderBy(c => c.Name)
                .ToList();

            var categoriesAsJson = JsonConvert.SerializeObject(categories);

            WriteToFile("categories-by-products.json", categoriesAsJson);
        }

        private static void SuccessfullySoldProducts(ProductsShopDbContext context)
        {
            var usersWithSales = context.Users
                .Where(u => u.ProductsSold.Count() > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserDto>()
                .ToList();

            var usersWithSalesJson = JsonConvert.SerializeObject(usersWithSales);

            WriteToFile("users-sold-products.json", usersWithSalesJson);
        }

        private static void ProductsInRange(ProductsShopDbContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductDto>()
                .ToList();

            var productsInRangeJson = JsonConvert.SerializeObject(productsInRange);

            WriteToFile("products-in-range.json", productsInRangeJson);
        }

        private static void WriteToFile(string fileName, string fileContent)
        {
            string exportsFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Exports";
            //string expotsFolder = @"C:\Users\yanislav\Documents\Visual Studio 2017\Projects\JSONProcessing\ProductsShop.Client\Exports";
            string filePath = $"{exportsFolder}\\{fileName}";

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(fileContent);
            }
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.Seller,
                                opt => opt.MapFrom(src => src.Seller.FirstName + " " + src.Seller.LastName));

                cfg.CreateMap<Product, ProductWithBuyerDto>()
                    .ForMember(dest => dest.BuyerFirstName,
                                opt => opt.MapFrom(src => src.Buyer.FirstName))
                    .ForMember(dest => dest.BuyerLastName,
                                opt => opt.MapFrom(src => src.Buyer.LastName));

                cfg.CreateMap<User, UserDto>()
                    .ForMember(dest => dest.ProductsSold,
                                opt => opt.MapFrom(src => src.ProductsSold));
            });
        }
    }
}