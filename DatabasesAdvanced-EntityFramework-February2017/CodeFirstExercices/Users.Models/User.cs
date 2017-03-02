using System;
using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisteredOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastTimeLoggedIn { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }
    }
}
