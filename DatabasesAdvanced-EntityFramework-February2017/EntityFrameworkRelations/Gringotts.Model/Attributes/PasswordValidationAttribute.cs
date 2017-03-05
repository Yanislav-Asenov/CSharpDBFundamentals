namespace Gringotts.Model.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string stringValue = (string)value;
            Regex lowerCaseCheckRegex = new Regex(@"[a-z]{1,}");
            Regex upperCaseCheckRegex = new Regex(@"[A-Z]{1,}");
            Regex numberCheckRegex = new Regex(@"[0-9]{1,}");
            Regex symbolCheckRegex = new Regex(@"[!@#$%^&*()_+<>?]{1,}");

            if (!lowerCaseCheckRegex.IsMatch(stringValue) ||
                !upperCaseCheckRegex.IsMatch(stringValue) ||
                !numberCheckRegex.IsMatch(stringValue) ||
                !symbolCheckRegex.IsMatch(stringValue))
            {
                return false;
            }

            return true;
        }
    }
}
