namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? SellerId { get; set; }
        
        [ForeignKey("SellerId")]
        public User Seller { get; set; }

        public int? BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}
