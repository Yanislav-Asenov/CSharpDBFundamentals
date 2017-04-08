namespace TeamBuilder.Data.Validators
{
    using System.Collections.Generic;

    /// <summary>
    /// Result of validation check.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class. 
        /// Validation result by default is set to true!
        /// </summary>
        public ValidationResult()
        {
            this.IsValid = true;
            this.ValidationErrors = new List<string>();
        }

        /// <summary>
        /// Gets the overall validation result.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets all validation errors (if any).
        /// </summary>
        public ICollection<string> ValidationErrors { get; set; }
    }
}
