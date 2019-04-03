using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class Store
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public Products Product { get; set; }
    }
}
