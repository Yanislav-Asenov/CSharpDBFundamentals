namespace FindLast10Projects
{
    using Softuni.Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    class FindLast10Projects
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var projects = dbContext.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                })
                .ToList();

            StringBuilder content = new StringBuilder();
            foreach (var p in projects.OrderBy(p => p.Name))
            {
                content.AppendFormat("{0} {1} {2}{3}",
                    p.Name,
                    p.Description,
                    p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                    Environment.NewLine);
            }

            Console.WriteLine(content);
        }
    }
}
