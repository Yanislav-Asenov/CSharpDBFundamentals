namespace Projection.Migrations
{
    using Projection.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Projection.ProjectionDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Projection.ProjectionDbContext context)
        {
            if (context.Employees.Any())
            {
                return;
            }

            var employeesToInsert = new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Salary = 1111.99m,
                    Birthday = new DateTime(1980, 5, 5),
                    Address = "ul.PeshoPEshov",
                    Manager = new Employee()
                    {
                        FirstName = "Napesho",
                        LastName = "Managera",
                        Salary = 9999.99m,
                        Birthday = DateTime.Now,
                        Address = "ul.Manager.com",
                        Manager = null,
                        ManagerId = null
                    }
                },
                new Employee()
                {
                    FirstName = "Gosho",
                    LastName = "Goshov",
                    Salary = 2222.99m,
                    Birthday = new DateTime(1981, 5, 5),
                    Address = "ul.GoshoGoshov",
                    Manager = new Employee()
                    {
                        FirstName = "NagoshoGosho",
                        LastName = "ManageraNaGosho",
                        Salary = 9999.99m,
                        Birthday = DateTime.Now,
                        Address = "ul.Manager.com",
                        Manager = null,
                        ManagerId = null
                    }
                },
                new Employee()
                {
                    FirstName = "Stamat",
                    LastName = "Stamatov",
                    Salary = 3333.99m,
                    Birthday = new DateTime(1982, 5, 5),
                    Address = "ul.StamatStamatov",
                    Manager = new Employee()
                    {
                        FirstName = "NaStamatManagera",
                        LastName = "NaStamatManagera",
                        Salary = 9999.99m,
                        Birthday = DateTime.Now,
                        Address = "ul.Manager.com",
                        Manager = null,
                        ManagerId = null
                    }
                },
                new Employee()
                {
                    FirstName = "Peter",
                    LastName = "Petrov",
                    Salary = 4444.99m,
                    Birthday = new DateTime(1983, 5, 5),
                    Address = "ul.PeterPetrov",
                    Manager = null,
                    ManagerId = null
                },
                new Employee()
                {
                    FirstName = "Maria",
                    LastName = "Mariaova",
                    Salary = 5555.99m,
                    Birthday = new DateTime(1984, 5, 5),
                    Address = "ul.MariaMariova",
                    Manager = new Employee()
                    {
                        FirstName = "ManagerNaMaria",
                        LastName = "ManagerNaMaria",
                        Salary = 9999.99m,
                        Birthday = DateTime.Now,
                        Address = "ul.Manager.com",
                        Manager = null,
                        ManagerId = null
                    }
                },
                new Employee()
                {
                    FirstName = "Grigor",
                    LastName = "Grigorov",
                    Salary = 6666.99m,
                    Birthday = new DateTime(1985, 5, 5),
                    Address = "ul.GrigorGrigorov",
                    Manager = null,
                    ManagerId = null
                },
                new Employee()
                {
                    FirstName = "Ivo",
                    LastName = "Ivo",
                    Salary = 7777.99m,
                    Birthday = new DateTime(1986, 5, 5),
                    Address = "ul.IvoIvo",
                    Manager = null,
                    ManagerId = null
                },
                new Employee()
                {
                    FirstName = "Filip",
                    LastName = "Filipov",
                    Salary = 8888.99m,
                    Birthday = new DateTime(1987, 5, 5),
                    Address = "ul.FilipFilipov",
                    Manager = new Employee()
                    {
                        FirstName = "ManagerNaFilip",
                        LastName = "ManagerNaFilipov",
                        Salary = 9999.99m,
                        Birthday = DateTime.Now,
                        Address = "ul.Manager.com",
                        Manager = null,
                        ManagerId = null
                    }
                }
            };

            context.Employees.AddRange(employeesToInsert);
            context.SaveChanges();
        }
    }
}
