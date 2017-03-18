namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    public class LoginUserCommand : Command
    {
        public override string Execute(string[] commandParameters)
        {
            string username = commandParameters[0];
            string userPassword = commandParameters[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (Engine.IsUserLoggedIn())
                {
                    throw new ArgumentException("You should logout first!");
                }

                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null || user.Password != userPassword)
                {
                    throw new ArgumentException("Invalid username or password!");
                }

                Engine.CurrentUser = user;
            }

            return $"User [{username}] successfully logged in!";
        }
    }
}
