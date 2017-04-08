namespace TeamBuilder.Client.Console.Core
{
    using System;
    using TeamBuilder.Client.Console.Utilities;
    using TeamBuilder.Models;

    public static class AuthenticationManager
    {
        private static User currentUser;

        public static void Authorize()
        {
            if (currentUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static void Login(User user)
        {
            currentUser = user;
        }

        public static void Logout()
        {
            Authorize();

            currentUser = null;
        }

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
