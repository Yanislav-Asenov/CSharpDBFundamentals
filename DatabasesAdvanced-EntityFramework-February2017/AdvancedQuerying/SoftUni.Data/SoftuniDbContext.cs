namespace BookShopSystem.Data
{
    using SoftUni.Data;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;

    public class SoftuniDbContext : DbContext
    {
        public SoftuniDbContext()
            : base("name=SoftUniEntities")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public IEnumerable<usp_GetProjectsForEmployee_Result> GetProjectsForEmployee(string firstName, string lastName)
        {
            SqlParameter firstNameParameter = new SqlParameter("@FirstName", firstName);
            SqlParameter lastNameParameter = new SqlParameter("@LastName", lastName);
            var result = this.Database
                .SqlQuery<usp_GetProjectsForEmployee_Result>("exec usp_GetProjectsForEmployee @FirstName, @LastName",
                    firstNameParameter,
                    lastNameParameter)
                .ToList();

            return result;
        }
    }
}