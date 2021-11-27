using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class CreditCard
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public short Cvc { get; set; }
        public string CardHolderName { get; set; }
        public int CardNumber { get; set; }
        public short ExpMonth { get; set; }
        public short ExpYear { get; set; }
    }
}
