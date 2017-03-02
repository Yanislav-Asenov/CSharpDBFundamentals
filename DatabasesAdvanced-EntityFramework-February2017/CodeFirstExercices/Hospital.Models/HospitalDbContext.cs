namespace Hospital.Models
{
    using System.Data.Entity;

    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
            : base("name=HospitalDbContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<HospitalDbContext>());
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Visit> Visitations { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
    }
}