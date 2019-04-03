using System;
using System.Collections.Generic;

namespace MVC_Management.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public int Payment { get; set; }
        public int Delivery { get; set; }

        public Clients Client { get; set; }
        public Delivery DeliveryNavigation { get; set; }
        public Payment PaymentNavigation { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
