namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class UploadPictureCommand : Command
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public override string Execute(string[] commandParameters)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = commandParameters[0];
            string pictureTitle = commandParameters[1];
            string pictureFilePath = commandParameters[2];

            using(PhotoShareContext context = new PhotoShareContext())
            {
                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException($"Album [{albumName}] not found!");
                }

                bool isOwner = album.AlbumRoles
                    .Any(ar => ar.User?.Id == Engine.CurrentUser.Id && 
                        ar.Role == Role.Owner);

                if (!isOwner)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                album.Pictures.Add(new Picture
                {
                    Title = pictureTitle,
                    Path = pictureFilePath
                });

                context.SaveChanges();
            }

            return $"Picture [{pictureTitle}] added to [{albumName}]!";
        }
    }
}
