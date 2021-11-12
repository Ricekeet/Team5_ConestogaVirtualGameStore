using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItem = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public string UserId { get; set; }
        public double Subtotal { get; set; }
        public double? TaxPercent { get; set; }
        public double Total { get; set; }

        public virtual ICollection<CartItem> CartItem { get; set; }
    }
}
