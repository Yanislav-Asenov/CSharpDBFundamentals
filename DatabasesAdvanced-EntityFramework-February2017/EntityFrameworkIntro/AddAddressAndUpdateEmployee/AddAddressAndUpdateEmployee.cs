namespace AddAddressAndUpdateEmployee
{
    using Softuni.Data;
    using System;
    using System.Linq;

    public class AddAddressAndUpdateEmployee
    {
        private static readonly SoftuniContext _dbContext = new SoftuniContext();

        public static void Main()
        {
            try
            {
                var addres = new Address() { AddressText = "Vitoshka 15", TownID = 4 };
                UpdateEmployee("Nakov", addres);
                var employeeAddressTexts = _dbContext
                    .Employees
                    .OrderByDescending(e => e.AddressID)
                    .Select(e => e.Address.AddressText)
                    .Take(10)
                    .ToList();

                employeeAddressTexts.ForEach(at => Console.WriteLine(at));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void UpdateEmployee(string employeeLastName, Address address)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(e => e.LastName == employeeLastName);

                if (employee == null)
                {
                    System.Console.WriteLine("No employee with that last name");
                    return;
                }

                employee.Address = address;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void AddAddress(Address address)
        {
            try
            {
                _dbContext.Addresses.Add(address);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
