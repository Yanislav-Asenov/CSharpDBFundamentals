namespace TeamBuilder.Data.Validators
{
    using TeamBuilder.Models;

    public class EventValidator
    {
        public ValidationResult IsValid(Event teamEvent)
        {
            ValidationResult result = new ValidationResult();

            if (teamEvent == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Event cannot be null.");
            }

            if (string.IsNullOrEmpty(teamEvent.Name))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name cannot be null.");
            }

            if (teamEvent.Name.Length > 25)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name not valid.");
            }

            if (!string.IsNullOrEmpty(teamEvent.Description))
            {
                if (teamEvent.Description.Length > 250)
                {
                    result.IsValid = false;
                    result.ValidationErrors.Add("Description not valid.");
                }
            }

            if (teamEvent.StartDate > teamEvent.EndDate)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Start date not valid.");
            }



            return result;
        }
    }
}
