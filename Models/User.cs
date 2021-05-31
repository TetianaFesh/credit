using System;
using System.Collections.Generic;

#nullable disable

namespace Credit.Models
{
    public partial class User
    {
        public User()
        {
            DebtPayments = new HashSet<DebtPayment>();
            SubmittedApplications = new HashSet<SubmittedApplication>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ProbabilityOfInsolvency { get; set; }
        public string TypeOfUser { get; set; }
        public string Password { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<DebtPayment> DebtPayments { get; set; }
        public virtual ICollection<SubmittedApplication> SubmittedApplications { get; set; }
    }
}
