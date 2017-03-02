namespace Hospital.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Visit
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public int? DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public virtual List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
