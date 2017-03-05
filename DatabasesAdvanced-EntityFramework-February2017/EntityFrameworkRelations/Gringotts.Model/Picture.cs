namespace Gringotts.Model
{
    using System.Collections.Generic;

    public class Picture
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
