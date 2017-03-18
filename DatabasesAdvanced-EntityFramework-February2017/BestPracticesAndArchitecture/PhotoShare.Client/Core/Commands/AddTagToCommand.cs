namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class AddTagToCommand : Command
    {
        // AddTagTo <albumName> <tag>
        public override string Execute(string[] commandParameters)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = commandParameters[0];
            string tagName = commandParameters[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var album = context.Albums.FirstOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    throw new ArgumentException("Album do not exist!");
                }

                bool isOwner = album.AlbumRoles.Any(ar => ar.Role == Role.Owner && Engine.CurrentUser.Id == ar.User?.Id);

                if (!isOwner)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                var tag = context.Tags.FirstOrDefault(t => t.Name == tagName);

                if (tag == null)
                {
                    throw new ArgumentException("Tag do not exist!");
                }

                album.Tags.Add(tag);
                context.SaveChanges();
            }

            return $"Tag [{tagName}] added to [{albumName}]!";
        }
    }
}
