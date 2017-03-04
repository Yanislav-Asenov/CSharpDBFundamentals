namespace StudentSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }

        public DateTime? BirthDay { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public ICollection<Homework> Homeworks { get; set; } = new HashSet<Homework>();
    }
}
