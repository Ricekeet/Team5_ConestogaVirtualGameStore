using System;
using System.Collections.Generic;
using Team5_ConestogaVirtualGameStore.Controllers;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Team5_ConestogaVirtualGameStore.Models
{
    public partial class AspNetUsers
    {
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
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
