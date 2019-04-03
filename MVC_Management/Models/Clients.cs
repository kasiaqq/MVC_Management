using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class Clients
    {
        public Clients()
        {
            Orders = new HashSet<Orders>();
        }

        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
