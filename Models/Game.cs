using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Game
    {
        public int GameId { get; set; }
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double Price { get; set; }
        public int? Inventory { get; set; }
        public double? DiscountPercent { get; set; }
        public string Description { get; set; }
        public int ReviewListId { get; set; }

        public virtual Platform Platform { get; set; }
    }
}
