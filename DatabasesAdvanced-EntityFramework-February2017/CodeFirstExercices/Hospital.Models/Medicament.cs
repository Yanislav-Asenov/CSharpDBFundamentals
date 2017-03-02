namespace Hospital.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Medicament
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        public virtual List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
