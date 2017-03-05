namespace Gringotts.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    BackgroundColor = c.String(),
                    IsPublic = c.Boolean(nullable: false),
                    UserId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.Pictures",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(),
                    Path = c.String(),
                })
                .PrimaryKey(t => t.Id);

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
                "dbo.AlbumPictures",
                c => new
                {
                    Album_Id = c.Int(nullable: false),
                    Picture_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Album_Id, t.Picture_Id })
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.Picture_Id);

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
            DropForeignKey("dbo.Albums", "UserId", "dbo.Users");
            DropForeignKey("dbo.AlbumPictures", "Picture_Id", "dbo.Pictures");
            DropForeignKey("dbo.AlbumPictures", "Album_Id", "dbo.Albums");
            DropIndex("dbo.UserFriends", new[] { "Friend_Id" });
            DropIndex("dbo.UserFriends", new[] { "User_Id" });
            DropIndex("dbo.AlbumPictures", new[] { "Picture_Id" });
            DropIndex("dbo.AlbumPictures", new[] { "Album_Id" });
            DropIndex("dbo.Albums", new[] { "UserId" });
            DropTable("dbo.UserFriends");
            DropTable("dbo.AlbumPictures");
            DropTable("dbo.Users");
            DropTable("dbo.Pictures");
            DropTable("dbo.Albums");
        }
    }
}
