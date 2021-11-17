using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class Event
    {
        public Event()
        {
            JoinedEvent = new HashSet<JoinedEvent>();
        }

        public int EventId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public string EventPic { get; set; }

        public virtual ICollection<JoinedEvent> JoinedEvent { get; set; }
    }
}
