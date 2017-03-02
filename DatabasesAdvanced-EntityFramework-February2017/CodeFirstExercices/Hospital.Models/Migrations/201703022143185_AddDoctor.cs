namespace Hospital.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddDoctor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 50),
                    Specialty = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Visits", "DoctorId", c => c.Int());
            CreateIndex("dbo.Visits", "DoctorId");
            AddForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Visits", new[] { "DoctorId" });
            DropColumn("dbo.Visits", "DoctorId");
            DropTable("dbo.Doctors");
        }
    }
}
