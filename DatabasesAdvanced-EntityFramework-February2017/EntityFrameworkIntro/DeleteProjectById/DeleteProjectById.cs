namespace DeleteProjectById
{
    using Softuni.Data;
    using System;
    using System.Linq;
    using System.Text;

    class DeleteProjectById
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var projectForDelete = dbContext.Projects.Find(2);

            foreach (var employee in projectForDelete.Employees)
            {
                employee.Projects.Remove(projectForDelete);
            }

            dbContext.Projects.Remove(projectForDelete);
            dbContext.SaveChanges();

            var resultProjects = dbContext.Projects.Take(10).Select(p => p.Name).ToList();
            StringBuilder content = new StringBuilder();
            resultProjects.ForEach(p =>
            {
                content.AppendLine(p);
            });

            Console.Write(content);
        }
    }
}
