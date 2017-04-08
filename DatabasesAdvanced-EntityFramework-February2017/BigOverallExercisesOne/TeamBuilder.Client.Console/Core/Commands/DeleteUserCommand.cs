namespace TeamBuilder.Client.Console.Core.Commands
{
    using TeamBuilder.Client.Console.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class DeleteUserCommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);
            AuthenticationManager.Authorize();

            User currentUser = AuthenticationManager.GetCurrentUser();

            using (TeamBuilderDbContext context = new TeamBuilderDbContext())
            {
                context.Users.Attach(currentUser);
                currentUser.IsDeleted = true;
                context.SaveChanges();

                AuthenticationManager.Logout();
            }

            return $"User {currentUser.Username} was deleted succesfully!";
        }
    }
}
