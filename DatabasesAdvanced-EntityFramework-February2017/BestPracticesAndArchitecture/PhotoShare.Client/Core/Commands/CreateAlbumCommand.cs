namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class CreateAlbumCommand : Command
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        
        public override string Execute(string[] commandParameters)
        {
            string username = commandParameters[0];
            string albumTitle = commandParameters[1];
            string bgColor = commandParameters[2];
            string[] tags = commandParameters.Skip(3).ToArray();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User [{username}] not foud!");
                }

                if (user.AlbumRoles.Any(ar => ar.Album.Name == albumTitle))
                {
                    throw new ArgumentException($"Album [{albumTitle}] already exists!");
                }

                if (!Enum.GetNames(typeof(Color)).Contains(bgColor))
                {
                    throw new ArgumentException($"Color [{bgColor}] not found!");
                }

                foreach (var tag in tags)
                {
                    var tagFromDb = context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (tagFromDb == null)
                    {
                        throw new ArgumentException($"Invalid tags!");
                    }
                }

                Color backgroundColor = (Color)Enum.Parse(typeof(Color), bgColor);

                var album = new Album()
                {
                    Name = albumTitle,
                    Tags = tags.Select(x => new Tag { Name = x }).ToList(),
                    BackgroundColor = backgroundColor
                };

                user.AlbumRoles.Add(new AlbumRole()
                {
                    Album = album,
                    Role = Role.Owner
                });

                context.SaveChanges();
            }

            return $"Album [{albumTitle}] successfully created!";
        }
    }
}
