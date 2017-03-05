namespace BookmakerSystem.Model.Attributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PredictionValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            HashSet<string> allowedValues = new HashSet<string>()
            {
                "Home Team Win",
                "Draw Game",
                "Away Team Win"
            };

            if (allowedValues.Contains(value.ToString()))
            {
                return true;
            }

            return false;
        }
    }
}
