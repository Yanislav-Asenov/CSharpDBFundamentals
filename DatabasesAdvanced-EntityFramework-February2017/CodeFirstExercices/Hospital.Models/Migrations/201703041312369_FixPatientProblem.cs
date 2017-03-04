namespace Hospital.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixPatientProblem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VisitPatients", "Visit_Id", "dbo.Visits");
            DropForeignKey("dbo.VisitPatients", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.VisitPatients", new[] { "Visit_Id" });
            DropIndex("dbo.VisitPatients", new[] { "Patient_Id" });
            AddColumn("dbo.Visits", "Patient_Id", c => c.Int());
            CreateIndex("dbo.Visits", "Patient_Id");
            AddForeignKey("dbo.Visits", "Patient_Id", "dbo.Patients", "Id");
            DropTable("dbo.VisitPatients");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.VisitPatients",
                c => new
                {
                    Visit_Id = c.Int(nullable: false),
                    Patient_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Visit_Id, t.Patient_Id });

            DropForeignKey("dbo.Visits", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Visits", new[] { "Patient_Id" });
            DropColumn("dbo.Visits", "Patient_Id");
            CreateIndex("dbo.VisitPatients", "Patient_Id");
            CreateIndex("dbo.VisitPatients", "Visit_Id");
            AddForeignKey("dbo.VisitPatients", "Patient_Id", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VisitPatients", "Visit_Id", "dbo.Visits", "Id", cascadeDelete: true);
        }
    }
}
