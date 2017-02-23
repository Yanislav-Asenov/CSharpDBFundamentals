namespace EmployeesFromSeattle
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class EmployeesFromSeattle
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var employees = dbContext.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    DepartmentName = e.Department.Name,
                    Salary = e.Salary
                })
                .ToList();

            StringBuilder content = new StringBuilder();
            foreach (var e in employees)
            {
                content.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");
            }
            Console.Write(content);
        }
    }
}
