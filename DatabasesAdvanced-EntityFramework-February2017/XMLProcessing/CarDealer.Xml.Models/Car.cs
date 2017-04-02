namespace CarDealer.Xml.Models
{
    using System.Collections.Generic;

    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public double TravelledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<Part> Parts { get; set; } = new HashSet<Part>();
    }
}
