namespace TeamBuilder.Data.Validators
{
    using System.Linq;
    using TeamBuilder.Models;

    public class UserValidator
    {
        public ValidationResult IsValid(User user)
        {
            ValidationResult result = new ValidationResult();

            if (user == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("User cannot be null");

                return result;
            }

            if (user.Username == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Username is required.");
            }

            if (user.Username.Length < 3 || user.Username.Length > 25)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Username not valid.");
            }

            if (user.FirstName != null)
            {
                if (user.FirstName.Length > 25)
                {
                    result.IsValid = false;
                    result.ValidationErrors.Add("First name not valid.");
                }
            }

            if (user.LastName != null)
            {
                if (user.LastName.Length > 25)
                {
                    result.IsValid = false;
                    result.ValidationErrors.Add("Last name is not valid.");
                }
            }

            if (user.Password == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Password is required.");
            }

            if (!user.Password.Any(c => char.IsUpper(c)))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Password should contain atleast 1 uppercase letter.");
            }

            if (!user.Password.Any(c => char.IsDigit(c)))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Password should containt atleast 1 digit.");
            }

            if (user.Gender != Gender.Male && user.Gender != Gender.Female)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Gender can be 'Male' or 'Female'");
            }

            return result;
        }
    }
}
