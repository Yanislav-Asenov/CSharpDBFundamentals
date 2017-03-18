namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class LogoutUserCommand : Command
    {
        public override string Execute(string[] commandParameters)
        {
            if (!Engine.IsUserLoggedIn())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            string username = Engine.CurrentUser.Username;

            Engine.LogoutUser();

            return $"User {username} successfully logged out!";
        }
    }
}
