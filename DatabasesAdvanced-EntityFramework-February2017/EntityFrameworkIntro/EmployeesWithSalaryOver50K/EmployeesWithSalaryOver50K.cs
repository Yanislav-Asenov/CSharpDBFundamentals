namespace EmployeesWithSalaryOver50K
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    public class EmployeesWithSalaryOver50K
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();
            var employees = dbContext.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName)
                .ToList();

            StringBuilder content = new StringBuilder();
            employees.ForEach(e => content.AppendLine(e));
            Console.WriteLine(content);
        }
    }
}
