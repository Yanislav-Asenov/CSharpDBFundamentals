namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserRelationWIthEvents : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Events", "CreatorId");
            AddForeignKey("dbo.Events", "CreatorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CreatorId", "dbo.Users");
            DropIndex("dbo.Events", new[] { "CreatorId" });
        }
    }
}
