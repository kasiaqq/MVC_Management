using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Delivery1 { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
