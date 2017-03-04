namespace Sales.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreditCardNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        StoreLocationId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.StoreLocations", t => t.StoreLocationId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.StoreLocationId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.StoreLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoctionName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "StoreLocationId", "dbo.StoreLocations");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Sales", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Product_Id" });
            DropIndex("dbo.Sales", new[] { "StoreLocationId" });
            DropIndex("dbo.Sales", new[] { "CustomerId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropTable("dbo.StoreLocations");
            DropTable("dbo.Products");
            DropTable("dbo.Sales");
            DropTable("dbo.Customers");
        }
    }
}
