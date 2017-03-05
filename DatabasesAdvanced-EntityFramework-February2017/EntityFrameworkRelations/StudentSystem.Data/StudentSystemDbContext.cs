namespace StudentSystem.Data
{
    using Model;
    using System.Data.Entity;

    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()
            : base("name=StudentSystemDbContext")
        {
        }

        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Student> Students { get; set; }
        public IDbSet<Homework> Homeworks { get; set; }
        public IDbSet<License> Licenses { get; set; }
    }
}