using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.ViewModels
{
    public class WishListViewModel
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string UserId { get; set; }
        public string MineOrFriends { get; set; }
        public string UserName { get; set; }

        public virtual Game Game { get; set; }
    }
}
