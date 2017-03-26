namespace Projection
{
    using Projection.Models;
    using System.Data.Entity;

    public class ProjectionDbContext : DbContext
    {
        public ProjectionDbContext()
            : base("name=ProjectionDbContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}