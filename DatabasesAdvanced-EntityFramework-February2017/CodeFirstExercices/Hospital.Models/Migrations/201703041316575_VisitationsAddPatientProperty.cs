namespace Hospital.Models.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class VisitationsAddPatientProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Visits", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Visits", new[] { "DoctorId" });
            DropIndex("dbo.Visits", new[] { "Patient_Id" });
            RenameColumn(table: "dbo.Visits", name: "Patient_Id", newName: "PatientId");
            AlterColumn("dbo.Visits", "DoctorId", c => c.Int(nullable: true));
            AlterColumn("dbo.Visits", "PatientId", c => c.Int(nullable: true));
            CreateIndex("dbo.Visits", "DoctorId");
            CreateIndex("dbo.Visits", "PatientId");
            AddForeignKey("dbo.Visits", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Visits", "PatientId", "dbo.Patients");
            DropIndex("dbo.Visits", new[] { "PatientId" });
            DropIndex("dbo.Visits", new[] { "DoctorId" });
            AlterColumn("dbo.Visits", "PatientId", c => c.Int());
            AlterColumn("dbo.Visits", "DoctorId", c => c.Int());
            RenameColumn(table: "dbo.Visits", name: "PatientId", newName: "Patient_Id");
            CreateIndex("dbo.Visits", "Patient_Id");
            CreateIndex("dbo.Visits", "DoctorId");
            AddForeignKey("dbo.Visits", "DoctorId", "dbo.Doctors", "Id");
            AddForeignKey("dbo.Visits", "Patient_Id", "dbo.Patients", "Id");
        }
    }
}
