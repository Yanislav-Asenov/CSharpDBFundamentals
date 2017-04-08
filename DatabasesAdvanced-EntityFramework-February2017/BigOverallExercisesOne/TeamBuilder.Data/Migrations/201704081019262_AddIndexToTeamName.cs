namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToTeamName : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Teams", "Name", unique: true, name: "IX_Teams_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Teams", "IX_Teams_Name");
        }
    }
}
