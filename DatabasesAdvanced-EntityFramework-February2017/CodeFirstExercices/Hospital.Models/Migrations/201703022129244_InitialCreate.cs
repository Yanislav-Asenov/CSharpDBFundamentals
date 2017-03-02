namespace Hospital.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Comment = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Address = c.String(maxLength: 100),
                        Email = c.String(maxLength: 100),
                        DateOfBirth = c.DateTime(nullable: false),
                        Picture = c.Binary(),
                        IsMedicalInsured = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PatientDiagnosis",
                c => new
                    {
                        Patient_Id = c.Int(nullable: false),
                        Diagnosis_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_Id, t.Diagnosis_Id })
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Diagnosis", t => t.Diagnosis_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id)
                .Index(t => t.Diagnosis_Id);
            
            CreateTable(
                "dbo.MedicamentPatients",
                c => new
                    {
                        Medicament_Id = c.Int(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicament_Id, t.Patient_Id })
                .ForeignKey("dbo.Medicaments", t => t.Medicament_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Medicament_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.VisitPatients",
                c => new
                    {
                        Visit_Id = c.Int(nullable: false),
                        Patient_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visit_Id, t.Patient_Id })
                .ForeignKey("dbo.Visits", t => t.Visit_Id, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .Index(t => t.Visit_Id)
                .Index(t => t.Patient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitPatients", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.VisitPatients", "Visit_Id", "dbo.Visits");
            DropForeignKey("dbo.MedicamentPatients", "Patient_Id", "dbo.Patients");
            DropForeignKey("dbo.MedicamentPatients", "Medicament_Id", "dbo.Medicaments");
            DropForeignKey("dbo.PatientDiagnosis", "Diagnosis_Id", "dbo.Diagnosis");
            DropForeignKey("dbo.PatientDiagnosis", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.VisitPatients", new[] { "Patient_Id" });
            DropIndex("dbo.VisitPatients", new[] { "Visit_Id" });
            DropIndex("dbo.MedicamentPatients", new[] { "Patient_Id" });
            DropIndex("dbo.MedicamentPatients", new[] { "Medicament_Id" });
            DropIndex("dbo.PatientDiagnosis", new[] { "Diagnosis_Id" });
            DropIndex("dbo.PatientDiagnosis", new[] { "Patient_Id" });
            DropTable("dbo.VisitPatients");
            DropTable("dbo.MedicamentPatients");
            DropTable("dbo.PatientDiagnosis");
            DropTable("dbo.Visits");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Patients");
            DropTable("dbo.Diagnosis");
        }
    }
}
