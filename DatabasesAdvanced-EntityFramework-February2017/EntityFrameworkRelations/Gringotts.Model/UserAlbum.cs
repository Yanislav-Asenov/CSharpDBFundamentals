namespace Gringotts.Model
{
    public class UserAlbum
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }

        public string RolaType { get; set; }
    }
}
