namespace SimpleMapping
{
    using AutoMapper;
    using SimpleMapping.Dtos;
    using SimpleMapping.Models;
    using System;

    public class Startup
    {
        static void Main()
        {
            ConfigureAutoMapper();

            var employeeModel = new Employee()
            {
                FirstName = "Pesho",
                LastName = "Peshov",
                Salary = 999999999.99m,
                Birthday = DateTime.Now,
                Address = "ul. Pesho Peshov №999"
            };

            var employeeDto = Mapper.Map<EmployeeDto>(employeeModel);
            Console.WriteLine($"{employeeDto.FirstName} - {employeeDto.LastName} - {employeeDto.Salary}");
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
            });
        }
    }
}
