using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Management.Models
{
    public partial class Products
    {
        public Products()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Store = new HashSet<Store>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<Store> Store { get; set; }
    }
}
