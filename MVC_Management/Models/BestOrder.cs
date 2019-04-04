using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Management.Models
{
    public class BestOrder
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductName> Products { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
