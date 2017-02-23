namespace EmployeesFullInfo
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class EmployeesFullInfo
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();
            var employees = dbContext.Employees.OrderBy(e => e.EmployeeID).ToList();

            StringBuilder content = new StringBuilder();
            foreach (var e in employees)
            {
                content.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary}");
            }
            Console.WriteLine(content);
        }
    }
}
