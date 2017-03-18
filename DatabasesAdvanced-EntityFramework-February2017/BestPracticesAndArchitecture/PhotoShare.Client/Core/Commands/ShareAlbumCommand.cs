namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ShareAlbumCommand : Command
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public override string Execute(string[] commandParameters)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            int albumId = int.Parse(commandParameters[0].Trim());
            string userUsername = commandParameters[1].Trim();
            string permission = commandParameters[2].Trim();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var album = context.Albums.FirstOrDefault(a => a.Id == albumId);

                if (album == null)
                {
                    throw new ArgumentException($"Album [{albumId}] not found!");
                }

                bool isOwner = album.AlbumRoles.Any(ar => ar.User?.Id == Engine.CurrentUser.Id);

                if (!isOwner)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == userUsername);

                if (user == null)
                {
                    throw new ArgumentException($"User [{userUsername}] not found!");
                }

                if (permission != "Owner" && permission != "Viewer")
                {
                    throw new ArgumentException("Permission must be either \"Owner\" or \"Viewer\"");
                }

                if (context.AlbumRoles.Any(ar => ar.User.Id == user.Id))
                {
                    throw new ArgumentException($"User [{userUsername}] is already a [{permission}] of this album!");
                }

                context.AlbumRoles.Add(new AlbumRole()
                {
                    User = user,
                    Album = album,
                    Role = (Role)Enum.Parse(typeof(Role), permission)
                });
                context.SaveChanges();
            }

            return $"Username [{userUsername}] added to album [] ([{permission}])";
        }
    }
}
