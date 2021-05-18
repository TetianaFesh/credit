using System;
using System.Collections.Generic;

#nullable disable

namespace Credit.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
