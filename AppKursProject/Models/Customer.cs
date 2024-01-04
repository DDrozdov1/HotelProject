using System;
using System.Collections.Generic;

namespace AppKursProject.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Stays = new HashSet<Stay>();
        }

        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? PassportData { get; set; }

        public virtual ICollection<Stay> Stays { get; set; }
    }
}
