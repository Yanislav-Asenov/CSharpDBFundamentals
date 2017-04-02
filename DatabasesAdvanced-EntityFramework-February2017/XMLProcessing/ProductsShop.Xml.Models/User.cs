namespace ProductsShop.Xml.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<User> Friends { get; set; } = new HashSet<User>();

        public virtual ICollection<Product> ProductsBought { get; set; } = new HashSet<Product>();

        public virtual ICollection<Product> ProductsSold { get; set; } = new HashSet<Product>();
    }
}
