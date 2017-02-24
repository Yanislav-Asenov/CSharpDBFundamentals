namespace GetEmployeeWithId
{
    using Softuni.Data;
    using System.Linq;
    using System.Text;

    public class GetEmployeeWithId
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var employee = dbContext.Employees
                .Select(e => new
                {
                    EmployeeID = e.EmployeeID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle,
                    ProjectNames = e.Projects.OrderBy(p => p.Name).Select(p => p.Name)
                })
                .FirstOrDefault(e => e.EmployeeID == 147);

            StringBuilder content = new StringBuilder();
            content.AppendLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (var projectName in employee.ProjectNames)
            {
                content.AppendLine(projectName);
            }
            System.Console.WriteLine(content);
        }
    }
}
