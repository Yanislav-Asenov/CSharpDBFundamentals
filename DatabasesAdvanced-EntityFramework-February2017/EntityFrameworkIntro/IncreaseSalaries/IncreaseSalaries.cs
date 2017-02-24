namespace IncreaseSalaries
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    class IncreaseSalaries
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();
            string[] targetDepartments = new[] { "Engineering", "Tool Design", "Marketing", "Information Services" };
            var employeesForUpdate = dbContext.Employees
                .Where(e => targetDepartments.Contains(e.Department.Name))
                .ToList();

            employeesForUpdate.ForEach(e =>
            {
                e.Salary = e.Salary * 1.12m;
            });
            dbContext.SaveChanges();

            StringBuilder context = new StringBuilder();
            employeesForUpdate.ForEach(e =>
            {
                context.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary:F6})");
            });

            Console.WriteLine(context);
        }
    }
}
