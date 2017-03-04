namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Patient
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Upload)]
        public byte[] Picture { get; set; }

        public bool IsMedicalInsured { get; set; }

        public virtual ICollection<Visit> Visitations { get; set; } = new HashSet<Visit>();

        public virtual ICollection<Diagnosis> Diagnoses { get; set; } = new HashSet<Diagnosis>();

        public virtual ICollection<Medicament> PerscribedMedicaments { get; set; } = new HashSet<Medicament>();
    }
}
