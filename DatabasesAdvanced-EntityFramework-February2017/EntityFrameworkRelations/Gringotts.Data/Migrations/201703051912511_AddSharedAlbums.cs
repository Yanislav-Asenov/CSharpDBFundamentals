namespace Gringotts.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSharedAlbums : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "UserId", "dbo.Users");
            DropIndex("dbo.Albums", new[] { "UserId" });
            CreateTable(
                "dbo.UserAlbums",
                c => new
                {
                    User_Id = c.Int(nullable: false),
                    Album_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.User_Id, t.Album_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Album_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserAlbums", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.UserAlbums", "User_Id", "dbo.Users");
            DropIndex("dbo.UserAlbums", new[] { "Album_Id" });
            DropIndex("dbo.UserAlbums", new[] { "User_Id" });
            DropTable("dbo.UserAlbums");
            CreateIndex("dbo.Albums", "UserId");
            AddForeignKey("dbo.Albums", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
