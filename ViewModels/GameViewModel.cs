using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            CartItem = new HashSet<CartItem>();
            EventGameItem = new HashSet<EventGameItem>();
            OrderItem = new HashSet<OrderItem>();
            Review = new HashSet<Review>();
            WishlistItem = new HashSet<WishlistItem>();
        }

        public int GameId { get; set; }

        [Required]
        public IFormFile GameImg { get; set; }
        public int GenreId { get; set; }
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public int Inventory { get; set; }
        public double? DiscountPercent { get; set; }
        public string Description { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Platform Platform { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<EventGameItem> EventGameItem { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<WishlistItem> WishlistItem { get; set; }
    }
}
