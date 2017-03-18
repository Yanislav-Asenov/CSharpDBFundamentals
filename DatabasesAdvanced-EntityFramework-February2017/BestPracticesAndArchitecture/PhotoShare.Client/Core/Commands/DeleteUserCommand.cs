namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class DeleteUserCommand : Command
    {
        // DeleteUser <username>
        public override string Execute(string[] data)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User with {username} was not found!");
                }

                if (!Engine.IsUserLoggedIn(user.Username))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (user.IsDeleted ?? false)
                {
                    throw new InvalidOperationException($"User [{username}] is already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();

                Engine.LogoutUser();

                return $"User {username} was deleted from the database!";
            }
        }
    }
}
