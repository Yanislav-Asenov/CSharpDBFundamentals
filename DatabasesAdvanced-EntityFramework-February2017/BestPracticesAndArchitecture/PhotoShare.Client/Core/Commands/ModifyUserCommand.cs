namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class ModifyUserCommand : Command
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public override string Execute(string[] data)
        {
            string username = data[0];
            string targetProperty = data[1];
            string newValue = data[2];

            if (!Engine.IsUserLoggedIn() || !Engine.IsUserLoggedIn(username))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                ChangePropertyValue(targetProperty, user, newValue, context);

                return $"User {username} {targetProperty} is [{newValue}]";
            }
        }

        private void ChangePropertyValue(string targetProperty, User user, string newValue, PhotoShareContext context)
        {
            switch (targetProperty)
            {
                case "Password":
                    this.ChangePassword(user, newValue);
                    context.SaveChanges();
                    break;
                case "BornTown":
                    this.ChangeBornTown(user, newValue, context);
                    context.SaveChanges();
                    break;
                case "CurrentTown":
                    this.ChangeCurrentTown(user, newValue, context);
                    context.SaveChanges();
                    break;
                default:
                    throw new ArgumentException($"Property {targetProperty} not supported!");
            }
        }

        private void ChangeCurrentTown(User user, string newValue, PhotoShareContext context)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name == newValue);

            if (town == null)
            {
                throw new AggregateException($"Value {newValue} not valid.\n Town [{newValue}] not found!");
            }

            user.BornTown = town;
        }

        private void ChangeBornTown(User user, string newValue, PhotoShareContext context)
        {
            var town = context.Towns.FirstOrDefault(t => t.Name == newValue);

            if (town == null)
            {
                throw new AggregateException($"Value {newValue} not valid.\n Town [{newValue}] not found!");
            }

            user.BornTown = town;
        }

        private void ChangePassword(User user, string newValue)
        {
            if (!newValue.Any(c => char.IsLower(c)) || !newValue.Any(c => char.IsDigit(c)))
            {
                throw new AggregateException($"Value {newValue} not valid.\nInvalid Password!");
            }

            user.Password = newValue;
        }
    }
}
