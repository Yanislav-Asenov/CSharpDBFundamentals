namespace CodeFirstAdvanced
{
    using Sales.Models;
    using System.Linq;

    public class Startup
    {
        static void Main()
        {
            //InsertDataInLocalStore(); 01 - 02
            //InitializeAndSeedDataIntoSalesDb(); 03 - 04 - 05 - 06
            InitializeAndSeedDataIntoSalesDb();
        }

        static void InitializeAndSeedDataIntoSalesDb()
        {
            var salesDbContext = new SalesDbContext();
            var sales = salesDbContext.Sales.ToList();
        }

        //static void InsertDataInLocalStore()
        //{
        //    var productsToInsert = new List<Product>()
        //    {
        //        new Product
        //        {
        //            Name = "Random Product Name 1",
        //            Description = "Random Product Description 1",
        //            DistributorName = "Random DistributorName 1",
        //            Price = 999.99m,
        //            Weight = 50,
        //            Quantity = 5
        //        },
        //        new Product
        //        {
        //            Name = "Random Product Name 2",
        //            Description = "Random Product Description 2",
        //            DistributorName = "Random DistributorName 2",
        //            Price = 3333.33m,
        //            Weight = 30,
        //            Quantity = 100
        //        },
        //        new Product
        //        {
        //            Name = "Random Product Name 3",
        //            Description = "Random Product Description 3",
        //            DistributorName = "Random DistributorName 3",
        //            Price = 1999.99m,
        //            Weight = 900,
        //            Quantity = 1
        //        }
        //    };

        //    var dbContext = new LocalStoreDbContext();
        //    dbContext.Products.AddRange(productsToInsert);
        //    dbContext.SaveChanges();
        //}
    }
}
