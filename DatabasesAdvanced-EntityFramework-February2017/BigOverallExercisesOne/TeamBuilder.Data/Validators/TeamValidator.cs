namespace TeamBuilder.Data.Validators
{
    using TeamBuilder.Models;

    class TeamValidator
    {
        public ValidationResult IsValid(Team team)
        {
            ValidationResult result = new ValidationResult();

            if (team == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Team cannot be null.");
            }

            if (team.Name == null)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name cannot be null.");
            }

            if (team.Name.Length > 25)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Name not valid");
            }

            if (!string.IsNullOrEmpty(team.Description))
            {
                if (team.Description.Length > 32)
                {
                    result.IsValid = false;
                    result.ValidationErrors.Add("Description not valid.");
                }
            }

            if (string.IsNullOrEmpty(team.Acronym))
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Acronym cannot be null.");
            }

            if (team.Acronym.Length != 3)
            {
                result.IsValid = false;
                result.ValidationErrors.Add("Acronym not valid.");
            }

            return result;
        }
    }
}
