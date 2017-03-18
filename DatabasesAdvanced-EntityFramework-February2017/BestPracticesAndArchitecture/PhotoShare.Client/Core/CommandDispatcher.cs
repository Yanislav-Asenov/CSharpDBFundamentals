namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using PhotoShare.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CommandDispatcher
    {
        private Dictionary<string, Command> commands;

        public CommandDispatcher()
        {
            this.InitializeCommands();
        }

        public string DispatchCommand(string[] commandParameters)
        {
            string commandName = commandParameters[0];
            string[] parameters = commandParameters.Skip(1).ToArray();

            if (!this.commands.ContainsKey(commandName))
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }

            return this.commands[commandName].Execute(parameters);
        }

        private void InitializeCommands()
        {
            this.commands = new Dictionary<string, Command>()
            {
                ["RegisterUser"] = new RegisterUserCommand(),
                ["AddTown"] = new AddTownCommand(),
                ["ModifyUser"] = new ModifyUserCommand(),
                ["DeleteUser"] = new DeleteUserCommand(),
                ["AddTag"] = new AddTagCommand(),
                ["CreateAlbum"] = new CreateAlbumCommand(),
                ["AddTagTo"] = new AddTagToCommand(),
                ["MakeFriends"] = new MakeFriendsCommand(),
                ["ListFriends"] = new PrintFriendsListCommand(),
                ["ShareAlbum"] = new ShareAlbumCommand(),
                ["UploadPicture"] = new UploadPictureCommand(),
                ["Exit"] = new ExitCommand(),
                ["Login"] = new LoginUserCommand(),
                ["Logout"] = new LogoutUserCommand()
            };
        }
    }
}
