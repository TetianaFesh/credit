using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Credit.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email field is empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
