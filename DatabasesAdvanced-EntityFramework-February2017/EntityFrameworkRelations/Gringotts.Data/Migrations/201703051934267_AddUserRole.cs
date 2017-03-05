namespace Gringotts.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAlbums", "RolaType", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.UserAlbums", "RolaType");
        }
    }
}
