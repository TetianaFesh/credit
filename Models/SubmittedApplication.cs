using System;
using System.Collections.Generic;

#nullable disable

namespace Credit.Models
{
    public partial class SubmittedApplication
    {
        public int Id { get; set; }
        public DateTime DateOfApplicationSubmission { get; set; }
        public double LoanSize { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public int TypeOfLoanId { get; set; }

        public virtual TypeOfLoan TypeOfLoan { get; set; }
        public virtual User User { get; set; }
    }
}
