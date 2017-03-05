namespace StudentSystem.Data.Migrations
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            if (!context.Students.Any())
            {
                InsertStudents(context);
                InsertCoursesResourcesAndHomeworks(context);
            }
        }

        private void InsertCoursesResourcesAndHomeworks(StudentSystemDbContext context)
        {
            var students = context.Students.ToList();

            var coursesToInsert = new List<Course>()
            {
                new Course()
                {
                    Name = "Основи на програмирането",
                    Description = "Курсът дава начални умения по програмиране, необходими за всички технологични специалности в СофтУни. Това включва писане на програмен код на начално ниво, работа със среда за разработка (IDE) ...",
                    Price = 50,
                    StartDate = new DateTime(2017, 3, 20),
                    EndDate = new DateTime(2017, 5, 20),
                    Resources = new List<Resource>()
                    {
                        new Resource()
                        {
                            Name = "Основи на програмирането септември",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането октомври 123",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането септември 1234",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането октомври 1345",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането септември 123456",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането октомври 1234567",
                            ResouceType = "Курс",
                            Url = "www.softuni.bg"
                        }
                    },
                    Homeworks = new List<Homework>()
                    {
                        new Homework()
                        {
                            Content = "Задачи",
                            ContentType = "Задачи",
                            SubmissionDate = DateTime.Now,
                            Student = students[0]
                        },
                        new Homework()
                        {
                            Content = "Zadachi 2",
                            ContentType = "Mn gotino",
                            SubmissionDate = DateTime.Now,
                            Student = students[1]
                        },
                        new Homework()
                        {
                            Content = "Zadachi 3",
                            ContentType = "Content 3",
                            SubmissionDate = DateTime.Now,
                            Student = students[2]
                        }
                    },
                    Students = students.Take(3).ToList()
                },
                new Course()
                {
                    Name = "Основи на програмирането 2",
                    Description = "2 2 2Курсът дава начални умения по програмиране, необходими за всички технологични специалности в СофтУни. Това включва писане на програмен код на начално ниво, работа със среда за разработка (IDE) ...",
                    Price = 50,
                    StartDate = new DateTime(2017, 5, 20),
                    EndDate = new DateTime(2017, 7, 20),
                    Resources = new List<Resource>()
                    {
                        new Resource()
                        {
                            Name = "Основи на програмирането септември 2",
                            ResouceType = "Курс 2",
                            Url = "www.softuni.bg 2"
                        },
                        new Resource()
                        {
                            Name = "Основи на програмирането октомври 3",
                            ResouceType = "Курс 3",
                            Url = "www.softuni.bg 3"
                        }
                    },
                    Homeworks = new List<Homework>()
                    {
                        new Homework()
                        {
                            Content = "Задачи 1",
                            ContentType = "Задачи 1",
                            SubmissionDate = DateTime.Now,
                            Student = students[3]
                        },
                        new Homework()
                        {
                            Content = "Zadachi 2",
                            ContentType = "Mn gotino 2",
                            SubmissionDate = DateTime.Now,
                            Student = students[4]
                        }
                    },
                    Students = students.Skip(3).Take(2).ToList()
                }
            };

            foreach (var course in coursesToInsert)
            {
                context.Courses.AddOrUpdate(course);
            }

            context.SaveChanges();
        }

        private void InsertStudents(StudentSystemDbContext context)
        {
            var studentsToInsert = new List<Student>()
            {
                new Student()
                {
                    Name = "Gosho Goshov",
                    PhoneNumber = "088855511",
                    BirthDay = new DateTime(1995, 5, 5),
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    Name = "Pesho Peshov",
                    PhoneNumber = "1231231230",
                    BirthDay = new DateTime(1996, 5, 5),
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    Name = "Stamat Stamatov",
                    PhoneNumber = "1233211230",
                    BirthDay = new DateTime(1997, 5, 5),
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    Name = "Peter Petrov",
                    PhoneNumber = "321123222",
                    BirthDay = new DateTime(1998, 5, 5),
                    RegisteredOn = DateTime.Now
                },
                new Student()
                {
                    Name = "Ivo Ivo",
                    PhoneNumber = "32123123",
                    BirthDay = new DateTime(1999, 5, 5),
                    RegisteredOn = DateTime.Now
                }
            };

            foreach (var student in studentsToInsert)
            {
                context.Students.AddOrUpdate(student);
            }

            context.SaveChanges();
        }
    }
}
