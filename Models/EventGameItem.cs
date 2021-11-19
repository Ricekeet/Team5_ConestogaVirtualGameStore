using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class EventGameItem
    {
        public int ItemId { get; set; }
        public int EventId { get; set; }
        public int GameId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Game Game { get; set; }
    }
}
