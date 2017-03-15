namespace BookShopSystem.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
