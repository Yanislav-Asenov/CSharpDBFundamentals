namespace RemoveTowns
{
    using Softuni.Data;
    using System;
    using System.Linq;

    class RemoveTowns
    {
        static void Main()
        {
            var dbContext = new SoftuniContext();

            string townToDeleteName = Console.ReadLine();

            var townToDelete = dbContext.Towns.FirstOrDefault(t => t.Name == townToDeleteName);

            if (townToDelete == null)
            {
                Console.WriteLine("Town not found");
                return;
            }

            var townAddresses = townToDelete.Addresses.ToList();

            foreach (var townAddress in townAddresses)
            {
                var employeeAddresses = townAddress.Employees.ToList();
                foreach (var address in employeeAddresses)
                {
                    address.AddressID = null;
                }
            }

            dbContext.Addresses.RemoveRange(townToDelete.Addresses);
            dbContext.Towns.Remove(townToDelete);
            dbContext.SaveChanges();

            if (townAddresses.Count > 1)
            {
                Console.WriteLine($"{townAddresses.Count} addresses in {townToDeleteName} were deleted");
            }
            else
            {
                Console.WriteLine($"{townAddresses.Count} address in {townToDeleteName} were deleted");
            }
        }
    }
}
