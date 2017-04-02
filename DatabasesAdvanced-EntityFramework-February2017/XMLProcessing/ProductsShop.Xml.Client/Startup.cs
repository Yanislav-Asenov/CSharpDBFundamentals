namespace ProductsShop.Xml.Client
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ProductsShop.Xml.Client.Dtos;
    using ProductsShop.Xml.Data;
    using ProductsShop.Xml.Models;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            ConfigureAutoMapper();

            var context = new ProductsShopXmlDbContext();

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
            UsersAndProducts(context);
            #endregion
        }

        private static void UsersAndProducts(ProductsShopXmlDbContext context)
        {
            var usersAndProducts = context.Users
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    SoldProducts = u.ProductsSold
                        .Select(sp => new
                        {
                            sp.Name,
                            sp.Price
                        })
                    })
                .ToList();

            var usersAndProductsXmlDoc = new XDocument();

            usersAndProductsXmlDoc.Add(new XElement("users",
                new XAttribute("count", usersAndProducts.Count),
                usersAndProducts.Select(u => 
                    new XElement("user",
                        new XAttribute("first-name", u.FirstName),
                        new XAttribute("last-name", u.LastName),
                        new XAttribute("age", u.Age),
                        new XElement("sold-products", 
                            new XAttribute("count", u.SoldProducts.Count()),
                            u.SoldProducts.Select(
                                p => new XElement("product",
                                    new XAttribute("name", p.Name),
                                    new XAttribute("price", p.Price))
                                )
                            )
                        )
                    )
                )
            );

            Console.WriteLine(usersAndProductsXmlDoc);
            usersAndProductsXmlDoc.Save("../../Exports/users-and-products.xml");
        }

        private static void CategoriesByProducts(ProductsShopXmlDbContext context)
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

            var categoriesXmlDoc = new XDocument();

            categoriesXmlDoc.Add(new XElement("categories",
                categories.Select(c => 
                    new XElement("category",
                        new XAttribute("name", c.Name),
                        new XElement("products-count", c.ProductsCount),
                        new XElement("average-price", c.AveragePrice),
                        new XElement("total-revenue", c.TotalRevenue))
                    )
                )
            );

            Console.WriteLine(categoriesXmlDoc);
            categoriesXmlDoc.Save("../../Exports/categories-by-products.xml");
        }

        private static void SuccessfullySoldProducts(ProductsShopXmlDbContext context)
        {
            var usersWithSales = context.Users
                .Where(u => u.ProductsSold.Count() > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ProjectTo<UserDto>()
                .ToList();

            var xmlDoc = new XDocument();

            xmlDoc.Add(new XElement("users",
                usersWithSales.Select(u =>
                    new XElement("user",
                        new XAttribute("first-name", u.FirstName),
                        new XAttribute("last-name", u.LastName),
                        new XElement("sold-products",
                            u.ProductsSold.Select(sp =>
                                new XElement("product",
                                    new XElement("name", sp.Name),
                                    new XElement("price", sp.Price)
                                )
                            )
                        )
                    )
                )));

            Console.WriteLine(xmlDoc);
            xmlDoc.Save("../../Exports/users-sold-products.xml");
        }

        private static void ProductsInRange(ProductsShopXmlDbContext context)
        {
            var productsInRange = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .ProjectTo<ProductDto>()
                .ToList();

            var productsInRangeXmlDoc = new XDocument();

            productsInRangeXmlDoc.Add(new XElement("products",
                productsInRange.Select(p =>
                    new XElement("product",
                        new XAttribute("name", p.Name),
                        new XAttribute("price", p.Price),
                        new XAttribute("buyer", p.Buyer)
                        )
                    )
                )
            );

            Console.WriteLine(productsInRangeXmlDoc);
            productsInRangeXmlDoc.Save("../../Exports/products-in-range.xml");
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dest => dest.Buyer,
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
