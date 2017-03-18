namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;

    public class ExitCommand : Command
    {
        public override string Execute(string[] commandParameters)
        {
            Console.WriteLine("Bye-bye!");
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
