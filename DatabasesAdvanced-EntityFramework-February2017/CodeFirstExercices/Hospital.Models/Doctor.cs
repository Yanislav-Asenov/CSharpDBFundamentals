namespace Hospital.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Specialty { get; set; }

        public virtual ICollection<Visit> Visitations { get; set; } = new HashSet<Visit>();
    }
}
