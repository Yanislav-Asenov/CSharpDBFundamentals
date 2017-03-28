namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Part
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();
    }
}
