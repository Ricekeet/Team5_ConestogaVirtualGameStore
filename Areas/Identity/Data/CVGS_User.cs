using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CVGS_User class
    public class CVGS_User : IdentityUser
    {
        public string picFileName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressListID { get; set; }
        [ForeignKey("Genre")]
        public int FavoriteGenreID { get; set; }
        [ForeignKey("Platform")]
        public int FavoritePlatformID { get; set; }
        public bool PromotialEmails { get; set; }
        public string gender { get; set; }
        public DateTime birthday { get; set; }
    }
}
