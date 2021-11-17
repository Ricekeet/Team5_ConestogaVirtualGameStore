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
        public string picFileName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressListID { get; set; }
        public int FavoriteGenreID { get; set; }
        public int FavoritePlatformID { get; set; }
        public bool PromotialEmails { get; set; }
    }
}
