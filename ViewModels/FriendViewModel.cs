using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.ViewModels
{
    public class FriendViewModel
    {
        public int ItemId { get; set; }
        public int FriendType { get; set; }
        public string HostUserId { get; set; }
        public string FriendUserId { get; set; }
        public string FriendUserName { get; set; }

        public virtual FriendType FriendTypeNavigation { get; set; }
    }
}
