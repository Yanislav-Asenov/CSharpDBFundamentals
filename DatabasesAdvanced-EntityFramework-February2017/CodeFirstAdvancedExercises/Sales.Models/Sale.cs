namespace Sales.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Sale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int StoreLocationId { get; set; }
        public StoreLocation StoreLocation { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    }
}
