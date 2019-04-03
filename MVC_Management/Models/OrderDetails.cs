using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }

        public Orders Order { get; set; }
        public Products Product { get; set; }
    }
}
