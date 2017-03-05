namespace Gringotts.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 30),
                    Password = c.String(nullable: false, maxLength: 50),
                    Email = c.String(),
                    ProfilePicture = c.Binary(),
                    RegisteredOn = c.DateTime(nullable: false),
                    LastTimeLoggedIn = c.DateTime(nullable: false),
                    Age = c.Int(nullable: false),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.UserFriends",
                c => new
                {
                    User_Id = c.Int(nullable: false),
                    Friend_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.User_Id, t.Friend_Id })
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.Friend_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Friend_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserFriends", "Friend_Id", "dbo.Users");
            DropForeignKey("dbo.UserFriends", "User_Id", "dbo.Users");
            DropIndex("dbo.UserFriends", new[] { "Friend_Id" });
            DropIndex("dbo.UserFriends", new[] { "User_Id" });
            DropTable("dbo.UserFriends");
            DropTable("dbo.Users");
        }
    }
}
