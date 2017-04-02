namespace CarDealer.Xml.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InigitalCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Products", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Products", "SellerId", "dbo.Users");
            DropIndex("dbo.Products", new[] { "SellerId" });
            DropIndex("dbo.Products", new[] { "BuyerId" });
            DropIndex("dbo.UserUsers", new[] { "User_Id" });
            DropIndex("dbo.UserUsers", new[] { "User_Id1" });
            DropIndex("dbo.ProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.ProductCategories", new[] { "Category_Id" });
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Model = c.String(),
                        TravelledDistance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsImporter = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CarId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        IsYoungDriver = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarParts",
                c => new
                    {
                        Car_Id = c.Int(nullable: false),
                        Part_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Car_Id, t.Part_Id })
                .ForeignKey("dbo.Cars", t => t.Car_Id, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.Part_Id, cascadeDelete: true)
                .Index(t => t.Car_Id)
                .Index(t => t.Part_Id);
            
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.UserUsers");
            DropTable("dbo.ProductCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id });
            
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        User_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(nullable: false),
                        Age = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellerId = c.Int(),
                        BuyerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Sales", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CarParts", "Part_Id", "dbo.Parts");
            DropForeignKey("dbo.CarParts", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.Parts", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.CarParts", new[] { "Part_Id" });
            DropIndex("dbo.CarParts", new[] { "Car_Id" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "CarId" });
            DropIndex("dbo.Parts", new[] { "SupplierId" });
            DropTable("dbo.CarParts");
            DropTable("dbo.Customers");
            DropTable("dbo.Sales");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Parts");
            DropTable("dbo.Cars");
            CreateIndex("dbo.ProductCategories", "Category_Id");
            CreateIndex("dbo.ProductCategories", "Product_Id");
            CreateIndex("dbo.UserUsers", "User_Id1");
            CreateIndex("dbo.UserUsers", "User_Id");
            CreateIndex("dbo.Products", "BuyerId");
            CreateIndex("dbo.Products", "SellerId");
            AddForeignKey("dbo.Products", "SellerId", "dbo.Users", "Id");
            AddForeignKey("dbo.ProductCategories", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategories", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "BuyerId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users", "Id");
            AddForeignKey("dbo.UserUsers", "User_Id", "dbo.Users", "Id");
        }
    }
}
