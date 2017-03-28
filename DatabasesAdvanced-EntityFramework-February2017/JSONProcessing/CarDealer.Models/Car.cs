namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double TravelledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<Car> Parts { get; set; } = new HashSet<Car>();

        public ICollection<Supplier> Suppliers { get; set; } = new HashSet<Supplier>();
    }
}
