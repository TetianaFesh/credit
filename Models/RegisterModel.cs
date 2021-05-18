using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Credit.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name field is empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Second name field is empty")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Patronymic field is empty")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Address field is empty")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number field is empty")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "ProbabilityOfInsolvency field is empty")]
        public double ProbabilityOfInsolvency { get; set; }

        [Required(ErrorMessage = "Email field is empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password field is empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Both passwords should match")]
        public string ConfirmPassword { get; set; }
    }
}
