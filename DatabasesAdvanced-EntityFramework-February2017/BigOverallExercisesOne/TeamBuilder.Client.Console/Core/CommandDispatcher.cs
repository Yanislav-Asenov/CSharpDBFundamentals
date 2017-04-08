namespace TeamBuilder.Application.Core
{
    using System;
    using System.Linq;
    using TeamBuilder.Client.Console.Core.Commands;

    public class CommandDispatcher
    {
        public string Dispatch(string input)
        {
            string result = string.Empty;

            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand();
                    result = deleteUser.Execute(inputArgs);
                    break;
                case "Logout":
                    LogoutCommand logoutUser = new LogoutCommand();
                    result = logoutUser.Execute(inputArgs);
                    break;
                case "Login":
                    LoginCommand loginUser = new LoginCommand();
                    result = loginUser.Execute(inputArgs);
                    break;
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result  = registerUser.Execute(inputArgs);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    exit.Execute(inputArgs);
                    break;
                case "clear":
                    Console.Clear();
                    break;
                default:
                    throw new NotSupportedException($"Command {commandName} not supported!");
            }

            return result;
        }
    }
}
