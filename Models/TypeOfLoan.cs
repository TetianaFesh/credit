using System;
using System.Collections.Generic;

#nullable disable

namespace Credit.Models
{
    public partial class TypeOfLoan
    {
        public TypeOfLoan()
        {
            SubmittedApplications = new HashSet<SubmittedApplication>();
        }

        public int Id { get; set; }
        public string TypeOfLoanRate { get; set; }
        public double Percent { get; set; }

        public virtual ICollection<SubmittedApplication> SubmittedApplications { get; set; }
    }
}
