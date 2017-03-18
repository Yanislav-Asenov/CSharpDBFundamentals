namespace PhotoShare.Client.Core
{
    using PhotoShare.Models;
    using System;

    public class Engine
    {
        private readonly CommandDispatcher commandDispatcher;
        public static User CurrentUser;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    string result = this.commandDispatcher.DispatchCommand(data);
                    Console.WriteLine(result);
                    Console.WriteLine($"User: {CurrentUser?.Username}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null ? true : false;
        }

        public static bool IsUserLoggedIn(string username)
        {
            if (!IsUserLoggedIn())
            {
                return false;
            }

            return CurrentUser.Username == username ? true : false;
        }

        public static void LogoutUser()
        {
            CurrentUser = null;
        }
    }
}
