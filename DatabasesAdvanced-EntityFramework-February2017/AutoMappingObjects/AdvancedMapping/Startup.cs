namespace AdvancedMapping
{
    using AdvancedMapping.Dtos;
    using AdvancedMapping.Models;
    using AutoMapper;
    using System;
    using System.Collections.Generic;

    public class Startup
    {
        static void Main()
        {
            ConfigureAutoMapper();

            var managers = GetManagers();

            var managerDtos = Mapper.Map<IEnumerable<ManagerDto>>(managers);

            foreach (var manager in managerDtos)
            {
                Console.WriteLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.Employees.Count}");

                foreach (var employee in manager.Employees)
                {
                    Console.WriteLine($"    - {employee.FirstName} {employee.LastName} {employee.Salary}");
                }
            }
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<Employee, ManagerDto>();
            });
        }

        private static ICollection<Employee> GetManagers()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Salary = 9999.99m,
                    Birthday = DateTime.Now,
                    Address = "ul.PeshoPEshov",
                    IsOnHoliday = false,
                    Employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            FirstName = "Pesho",
                            LastName = "Peshov",
                            Salary = 1111.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.PeshoPEshov",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Gosho",
                            LastName = "Goshov",
                            Salary = 2222.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.GoshoGoshov",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Stamat",
                            LastName = "Stamatov",
                            Salary = 3333.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.StamatStamatov",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Peter",
                            LastName = "Petrov",
                            Salary = 4444.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.PeterPetrov",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Maria",
                            LastName = "Mariaova",
                            Salary = 5555.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.MariaMariova",
                            IsOnHoliday = false
                        }
                    },
                },
                new Employee()
                {
                    FirstName = "Pesho",
                    LastName = "Peshov",
                    Salary = 9999.99m,
                    Birthday = DateTime.Now,
                    Address = "ul.PeshoPEshov",
                    IsOnHoliday = false,
                    Employees = new List<Employee>()
                    {
                        new Employee()
                        {
                            FirstName = "Grigor",
                            LastName = "Grigorov",
                            Salary = 6666.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.GrigorGrigorov",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Ivo",
                            LastName = "Ivo",
                            Salary = 7777.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.IvoIvo",
                            IsOnHoliday = false
                        },
                        new Employee()
                        {
                            FirstName = "Filip",
                            LastName = "Filipov",
                            Salary = 8888.99m,
                            Birthday = DateTime.Now,
                            Address = "ul.FilipFilipov",
                            IsOnHoliday = false
                        }
                    }
                }
            };
        }
    }
}
