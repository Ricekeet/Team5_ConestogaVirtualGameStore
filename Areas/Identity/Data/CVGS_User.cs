using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Team5_ConestogaVirtualGameStore.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CVGS_User class
    public class CVGS_User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressListID { get; set; }
        public int FavoriteGenreID { get; set; }
        public int FavoritePlatformID { get; set; }
        public int PromotialEmails { get; set; }
        public int CartID { get; set; }
        public int WishlistID { get; set; }
    }
}
