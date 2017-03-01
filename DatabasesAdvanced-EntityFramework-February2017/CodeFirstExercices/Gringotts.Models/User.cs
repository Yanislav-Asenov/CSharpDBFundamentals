namespace Gringotts.Models
{
    using Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4,
            ErrorMessage = "Username must be between 4 and 30 characters")]
        public string Username { get; set; }

        [Required]
        [PasswordValidationAttribute]
        [StringLength(50, MinimumLength = 6,
            ErrorMessage = "Password must be between 6 and 50 characters")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        public byte[] ProfilePicture { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisteredOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
