using System;
using System.Collections.Generic;

#nullable disable

namespace Credit.Models
{
    public partial class DebtPayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double PaymentAmount { get; set; }
        public int UserId { get; set; }
        public string Paid { get; set; }

        public virtual User User { get; set; }
    }
}
