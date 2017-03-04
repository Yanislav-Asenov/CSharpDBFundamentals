namespace Sales.Models
{
    using System.Collections.Generic;

    public class StoreLocation
    {
        public int Id { get; set; }
        public string LoctionName { get; set; }
        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();
    }
}
