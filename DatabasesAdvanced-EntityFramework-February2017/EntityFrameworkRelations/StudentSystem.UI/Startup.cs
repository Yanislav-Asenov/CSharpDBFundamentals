namespace StudentSystem.UI
{
    using Data;
    using System;
    using System.Data.Entity.SqlServer;
    using System.Linq;
    using System.Text;

    class Startup
    {
        static void Main()
        {
            var context = new StudentSystemDbContext();

            #region // 01. Lists all students and their homework submissions
            // ListAllStudentsAndTheirHomeworkSubmissions(context);
            #endregion
            #region // 02. List all courses with their corresponding resources
            // ListAllCoursesWithTheirCorrespondingResources(context);
            #endregion
            #region // 03. List all courses with more than 5 resources
            // ListAllCoursesWithMoreThan5Resources(context);
            #endregion
            #region // 04. List all courses which were active on a given date
            // ListAllCoursesWhichWereActiveOnGivenDate(context, new DateTime(2017, 4, 20));
            #endregion
            #region // 05. For each student, calculate the number of courses he/she has enrolled 
            // CalculateTheNumberOfCoursesForEachStudent(context);
            #endregion
        }

        private static void CalculateTheNumberOfCoursesForEachStudent(StudentSystemDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    Name = s.Name,
                    NumberOfCourses = s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Price),
                    AveragePrice = s.Courses.Average(c => c.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.NumberOfCourses)
                .ThenBy(s => s.Name);

            StringBuilder content = new StringBuilder();
            foreach (var s in students)
            {
                content.AppendLine($"{s.Name} {s.NumberOfCourses} {s.TotalPrice} {s.AveragePrice}");
            }

            Console.Write(content);
        }

        private static void ListAllCoursesWhichWereActiveOnGivenDate(StudentSystemDbContext context, DateTime targetDate)
        {
            var courses = context.Courses
                .Where(c => targetDate <= c.StartDate && targetDate <= c.EndDate)
                .Select(c => new
                {
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    CourseDuration = SqlFunctions.DateDiff("day", c.StartDate, c.EndDate),
                    NumberOfStudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.NumberOfStudentsEnrolled)
                .ThenByDescending(c => c.CourseDuration)
                .ToList();

            StringBuilder content = new StringBuilder();

            foreach (var course in courses)
            {
                content.AppendFormat("{0} {1} {2} {3} - {4}{5}",
                    course.Name,
                    course.StartDate,
                    course.EndDate,
                    course.CourseDuration,
                    course.NumberOfStudentsEnrolled,
                    Environment.NewLine);
            }

            Console.Write(content);

        }

        private static void ListAllCoursesWithMoreThan5Resources(StudentSystemDbContext context)
        {
            var courses = context.Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    Name = c.Name,
                    ResourcesCount = c.Resources.Count
                });

            StringBuilder content = new StringBuilder();
            foreach (var course in courses)
            {
                content.AppendLine($"{course.Name} - {course.ResourcesCount}");
            }

            Console.Write(content);
        }

        private static void ListAllCoursesWithTheirCorrespondingResources(StudentSystemDbContext context)
        {
            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    Name = c.Name,
                    Description = c.Description,
                    Resources = c.Resources
                });


            StringBuilder content = new StringBuilder();
            foreach (var course in courses)
            {
                content.AppendLine($"{course.Name} {course.Description}");
                foreach (var resource in course.Resources)
                {
                    content.AppendLine($"--- {resource.Name} {resource.ResouceType} {resource.Url}");
                }
            }

            Console.Write(content);
        }

        private static void ListAllStudentsAndTheirHomeworkSubmissions(StudentSystemDbContext context)
        {
            var students = context.Students
                .Select(s => new
                {
                    StudentName = s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        Content = h.Content,
                        ContentType = h.ContentType
                    })
                });

            StringBuilder content = new StringBuilder();

            foreach (var student in students)
            {
                content.AppendLine(student.StudentName);
                foreach (var homework in student.Homeworks)
                {
                    content.AppendLine($"---{homework.Content} {homework.ContentType}");
                }
            }

            Console.Write(content);
        }
    }
}
