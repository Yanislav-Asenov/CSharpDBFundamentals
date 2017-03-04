namespace Sales.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DefaultValue(value: "No Description")]
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public ICollection<Product> Sales { get; set; } = new HashSet<Product>();
    }
}
