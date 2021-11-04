using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Platform
    {
        public Platform()
        {
            Game = new HashSet<Game>();
        }

        public int PlatformId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
