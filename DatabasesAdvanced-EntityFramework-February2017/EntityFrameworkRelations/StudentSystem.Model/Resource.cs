using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Model
{
    public class Resource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ResouceType { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
