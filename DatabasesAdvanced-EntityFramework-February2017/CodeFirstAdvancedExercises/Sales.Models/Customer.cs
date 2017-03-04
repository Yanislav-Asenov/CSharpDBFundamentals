namespace Sales.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string CreditCardNumber { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
