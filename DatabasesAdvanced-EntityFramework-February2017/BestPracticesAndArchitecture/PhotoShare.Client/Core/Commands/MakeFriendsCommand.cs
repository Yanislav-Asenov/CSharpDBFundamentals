namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class MakeFriendsCommand : Command
    {
        // MakeFriends <username1> <username2>
        public override string Execute(string[] commandParameters)
        {
            string firstUsername = commandParameters[0];
            string secondUsername = commandParameters[1];
            
            if (!Engine.IsUserLoggedIn() || !Engine.IsUserLoggedIn(firstUsername))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var firstUser = context.Users.FirstOrDefault(u => u.Username == firstUsername);

                if (firstUser == null)
                {
                    throw new ArgumentException($"[{firstUsername}] not found!");
                }

                var secondUser = context.Users.FirstOrDefault(u => u.Username == secondUsername);

                if (secondUser == null)
                {
                    throw new ArgumentException($"[{secondUsername}] not found!");
                }

                if (firstUser.Friends.Any(u => u.Id == secondUser.Id))
                {
                    throw new InvalidOperationException($"[{secondUsername}] is already a friend to [{firstUsername}]");
                }

                firstUser.Friends.Add(secondUser);
                secondUser.Friends.Add(firstUser);
                context.SaveChanges();
            }

            return $"Friend [{secondUsername}] added to [{firstUsername}]";
        }
    }
}
