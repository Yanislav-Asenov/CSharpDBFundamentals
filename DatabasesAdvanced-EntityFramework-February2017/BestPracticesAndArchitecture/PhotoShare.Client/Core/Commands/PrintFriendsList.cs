namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;
    using System.Text;

    public class PrintFriendsListCommand : Command
    {
        // PrintFriendsList <username>
        public override string Execute(string[] commandParameters)
        {
            string targetUserUsername = commandParameters[0];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == targetUserUsername);

                if (user == null)
                {
                    throw new ArgumentException($"User [{targetUserUsername}] not found!");
                }

                if (user.Friends.Count() == 0)
                {
                    return "No friends for this user. :(";
                }

                StringBuilder result = new StringBuilder();

                result.AppendLine("Friends:");
                foreach (var friend in user.Friends)
                {
                    result.AppendLine(friend.Username);
                }

                return result.ToString().Trim('\n');
            }
        }
    }
}
