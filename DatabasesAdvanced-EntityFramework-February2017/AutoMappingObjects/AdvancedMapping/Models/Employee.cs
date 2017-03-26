namespace AdvancedMapping.Models
{
    using System;
    using System.Collections.Generic;

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public bool IsOnHoliday { get; set; }
        public Employee Manager { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
