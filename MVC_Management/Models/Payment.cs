using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Payment1 { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
