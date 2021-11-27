using System;
using System.Collections.Generic;
using Team5_ConestogaVirtualGameStore.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime DateOrdered { get; set; }
        public double Total { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
