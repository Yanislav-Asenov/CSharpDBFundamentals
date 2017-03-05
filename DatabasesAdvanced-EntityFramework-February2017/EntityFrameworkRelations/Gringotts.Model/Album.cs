namespace Gringotts.Model
{
    using System.Collections.Generic;

    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
        public ICollection<UserAlbum> UserAlbums { get; set; }
        public ICollection<Picture> Pictures { get; set; } = new HashSet<Picture>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
