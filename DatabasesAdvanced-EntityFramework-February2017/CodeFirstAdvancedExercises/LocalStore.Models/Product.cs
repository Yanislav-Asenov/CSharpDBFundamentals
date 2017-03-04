namespace LocalStore.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [MaxLength(100)]
        public string DistributorName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public int Quantity { get; set; }
    }
}
