namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;
    using Utilities;

    public class AddTagCommand : Command
    {
        // AddTag <tag>
        public override string Execute(string[] data)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string tagName = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Tags.Any(tag => tag.Name == tagName))
                {
                    throw new ArgumentException($"Tag [{tagName}] already exists!");
                }

                context.Tags.Add(new Tag
                {
                    Name = tagName
                });

                context.SaveChanges();
            }

            return tagName + " was added successfully to database!";
        }
    }
}
