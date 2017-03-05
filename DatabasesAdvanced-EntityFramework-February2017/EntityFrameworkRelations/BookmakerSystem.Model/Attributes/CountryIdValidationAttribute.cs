namespace BookmakerSystem.Model.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class CountryIdValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string stringValue = value.ToString();

            if (stringValue.Length == 3)
            {
                return true;
            }

            return false;
        }
    }
}
