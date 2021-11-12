using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class FriendList
    {
        public FriendList()
        {
            FriendItem = new HashSet<FriendItem>();
        }

        public int ListId { get; set; }
        public string HostUserId { get; set; }

        public virtual ICollection<FriendItem> FriendItem { get; set; }
    }
}
