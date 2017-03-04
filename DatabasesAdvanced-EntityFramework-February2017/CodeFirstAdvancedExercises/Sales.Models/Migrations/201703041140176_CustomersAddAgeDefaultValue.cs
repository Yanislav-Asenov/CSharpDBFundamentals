namespace Sales.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CustomersAddAgeDefaultValue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Customers", "Age", a => a.Int(defaultValue: 20));
        }

        public override void Down()
        {
            AlterColumn("Customers", "Age", a => a.Int(defaultValue: 0));
        }
    }
}
