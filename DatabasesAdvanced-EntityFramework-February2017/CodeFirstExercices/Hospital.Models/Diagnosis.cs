namespace Hospital.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Diagnosis
    {
        public int Id { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public virtual ICollection<Patient> Patients { get; set; } = new HashSet<Patient>();
    }
}
