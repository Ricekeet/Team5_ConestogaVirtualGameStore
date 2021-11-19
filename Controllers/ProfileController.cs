using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Team5_ConestogaVirtualGameStore.Models;
using Team5_ConestogaVirtualGameStore.Data;
using Team5_ConestogaVirtualGameStore.Areas.Identity.Data;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly CVGS_IdentityContext _IdentityContext;

        public ProfileController(CVGS_IdentityContext context)
        {
            _IdentityContext = context;
        }
        public async Task<IActionResult> Profile(string userID)
        {
            var user = await _IdentityContext.Users.FindAsync(userID);

            return View(user);
        }
    }
}
