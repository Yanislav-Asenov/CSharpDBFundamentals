namespace NativeSQLQuery
{
    using Softuni.Data;
    using System;
    using System.Diagnostics;
    using System.Linq;

    class NativeSQLQuery
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            var timer = new Stopwatch();
            timer.Start();
            for (var i = 0; i < 1000; i++)
            {
                PrintNamesWithNativeQuery(dbContext);
            }
            timer.Stop();
            Console.WriteLine($"Native: {timer.Elapsed}");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            timer.Reset();
            timer.Start();
            for (var i = 0; i < 1000; i++)
            {
                PrintNamesWithLinq(dbContext);
            }
            timer.Stop();
            Console.WriteLine($"Linq: {timer.Elapsed}");
        }

        private static void PrintNamesWithLinq(SoftuniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year == 2002) > 0)
                .Select(e => e.FirstName)
                .Distinct()
                .ToList();
        }

        private static void PrintNamesWithNativeQuery(SoftuniContext context)
        {
            string query = "SELECT em.FirstName FROM Employees em " +
                           "JOIN EmployeesProjects emproj " +
                           "ON emproj.EmployeeId = em.EmployeeId " +
                           "JOIN Projects proj " +
                           "ON emproj.ProjectId = proj.ProjectId AND YEAR(proj.StartDate) = 2002 " +
                           "GROUP BY em.FirstName";
            var result = context.Database.SqlQuery<string>(query).ToList();
        }
    }
}
