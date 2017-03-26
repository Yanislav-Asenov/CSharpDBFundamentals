namespace Projection
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Projection.Dtos;
    using Projection.Models;
    using System;
    using System.Linq;

    public class Startup
    {
        static void Main()
        {
            ConfigureAutoMapper();

            var context = new ProjectionDbContext();

            context.Employees
                .Where(e => e.Birthday.Year < 1990)
                .ProjectTo<EmployeeDto>()
                .ToList()
                .ForEach(e => Console.WriteLine($"{e.FirstName} {e.LastName} {e.Salary} - Manager: {e.ManagerLastName}"));
        }

        private static void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>()
                    .ForMember(dest => dest.ManagerLastName,
                                opt => opt.MapFrom(src => src.Manager == null ? "[no manager]" : src.Manager.LastName));
            });
        }
    }
}
