namespace Gringotts.Model
{
    using System.Collections.Generic;

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
