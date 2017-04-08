namespace TeamBuilder.Client.Console.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.Client.Console.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class LoginCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);

            string username = inputArgs[0];
            string password = inputArgs[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            User user = this.GetUserByCredentials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }

            AuthenticationManager.Login(user);

            return $"User {user.Username} successfully logged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {
            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                return context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.IsDeleted == false);
            }
        }
    }
}
