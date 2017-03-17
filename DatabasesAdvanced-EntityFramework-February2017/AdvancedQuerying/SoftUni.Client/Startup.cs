namespace SoftUni.Client
{
    using BookShopSystem.Data;
    using System;
    using System.Linq;

    public class Startup
    {
        static void Main()
        {
            var context = new SoftuniDbContext();

            #region 16. Call A Stored Procedure
            //CallAStoredProcedure(context);
            #endregion
            #region 17. Employees Maximum Salaries
            //EmployeesMaximumSalaries(context);
            #endregion
        }

        private static void EmployeesMaximumSalaries(SoftuniDbContext context)
        {
            var result = context.Departments
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    DepartmentMaxSalary = d.Employees.Select(e => e.Salary).Max(x => x)
                })
                .Where(d => d.DepartmentMaxSalary < 30000 || d.DepartmentMaxSalary > 70000)
                .ToList();

            foreach (var department in result)
            {
                Console.WriteLine($"{department.DepartmentName} - {department.DepartmentMaxSalary}");
            }
        }

        private static void CallAStoredProcedure(SoftuniDbContext context)
        {
            var nameArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameArgs[0];
            var lastName = nameArgs[1];

            var result = context.GetProjectsForEmployee(firstName, lastName);

            foreach (var project in result)
            {
                Console.WriteLine($"{project.Name} - {project.Description} - {project.StartDate}");
            }
        }
    }
}
