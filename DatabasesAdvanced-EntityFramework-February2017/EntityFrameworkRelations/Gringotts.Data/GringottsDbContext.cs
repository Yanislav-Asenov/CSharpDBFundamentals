namespace Gringotts.Data
{
    using Model;
    using System.Data.Entity;

    public class GringottsDbContext : DbContext
    {
        public GringottsDbContext()
            : base("name=GringottsDbContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany();

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserAlbums)
                .WithRequired(ua => ua.User);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.UserAlbums)
                .WithRequired(ua => ua.Album);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Pictures)
                .WithMany(p => p.Albums);

            modelBuilder.Entity<Picture>()
                .HasMany(p => p.Albums)
                .WithMany(a => a.Pictures);

            modelBuilder.Entity<UserAlbum>()
                .HasKey(ua => new { ua.UserId, ua.AlbumId });

            modelBuilder.Entity<UserAlbum>()
                .HasRequired(ua => ua.User)
                .WithMany(u => u.UserAlbums);

            modelBuilder.Entity<UserAlbum>()
                .HasRequired(ua => ua.Album)
                .WithMany(a => a.UserAlbums);

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Picture> Pictures { get; set; }
        public IDbSet<Tag> Tags { get; set; }
        public IDbSet<UserAlbum> UserAlbums { get; set; }
    }
}