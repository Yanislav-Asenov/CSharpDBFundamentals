namespace FindEmployeesByFirstName
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    class FindEmployeesByFirstName
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();
            string pattern = "SA";
            var employees = dbContext.Employees
                .Where(e => e.FirstName.StartsWith(pattern))
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary
                })
                .ToList();

            StringBuilder content = new StringBuilder();
            employees.ForEach(e =>
            {
                content.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary})");
            });
            Console.WriteLine(content);
        }
    }
}
