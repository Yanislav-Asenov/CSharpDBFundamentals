using Softuni.Data;
using System;
using System.Linq;
using System.Text;

namespace Departments
{
    public class Departments
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var departments = dbContext.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count);

            StringBuilder content = new StringBuilder();
            foreach (var d in departments)
            {
                content.AppendLine($"{d.Name} {d.Manager.FirstName}");
                foreach (var e in d.Employees)
                {
                    content.AppendLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
                }
            }
            Console.Write(content);
        }
    }
}
