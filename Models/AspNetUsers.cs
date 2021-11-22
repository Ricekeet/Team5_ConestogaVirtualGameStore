using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            Address = new HashSet<Address>();
            CartItem = new HashSet<CartItem>();
            FriendItemFriendUser = new HashSet<FriendItem>();
            FriendItemHostUser = new HashSet<FriendItem>();
            JoinedEvent = new HashSet<JoinedEvent>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
            Review = new HashSet<Review>();
            WishlistItem = new HashSet<WishlistItem>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string PicFileName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? AddressListId { get; set; }
        public int? FavoriteGenreId { get; set; }
        public int? FavoritePlatformId { get; set; }
        public bool? PromotialEmails { get; set; }

        public virtual Genre FavoriteGenre { get; set; }
        public virtual Platform FavoritePlatform { get; set; }
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<FriendItem> FriendItemFriendUser { get; set; }
        public virtual ICollection<FriendItem> FriendItemHostUser { get; set; }
        public virtual ICollection<JoinedEvent> JoinedEvent { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<WishlistItem> WishlistItem { get; set; }
    }
}
