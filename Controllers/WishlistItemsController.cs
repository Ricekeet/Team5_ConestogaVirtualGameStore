using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team5_ConestogaVirtualGameStore.Models;
using Team5_ConestogaVirtualGameStore.ViewModels;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class WishlistItemsController : Controller
    {
        private readonly CVGS_Context _context;

        public WishlistItemsController(CVGS_Context context)
        {
            _context = context;
        }

        // GET: WishlistItems
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<WishListViewModel> wvm = new List<WishListViewModel>();

            // find list of friends
            var fiList = await _context.FriendItem.Where(w => w.HostUserId == userId).ToListAsync();

            foreach (FriendItem fi in fiList)
            {
                // find their userID and userName
                var aspNetUser = await _context.AspNetUsers
                    .FirstOrDefaultAsync(m => m.Id == fi.FriendUserId);

                var friendWI = _context.WishlistItem.Include(w => w.Game).Where(w => w.UserId.Contains(aspNetUser.Id));

                foreach (WishlistItem wi in friendWI)
                {
                    WishListViewModel vm = new WishListViewModel()
                    {
                        Game = wi.Game,
                        GameId = wi.GameId,
                        Id = wi.Id,
                        UserId = wi.UserId,
                        UserName = aspNetUser.UserName
                    };
                    wvm.Add(vm);
                }
            }

            // add to existing wishlistItem
            var cVGS_Context = _context.WishlistItem.Include(w => w.Game).Where(w=>w.UserId==userId.ToString());

            foreach(WishlistItem wi in cVGS_Context)
            {
                WishListViewModel vm = new WishListViewModel()
                {
                    Game = wi.Game,
                    GameId = wi.GameId,
                    Id = wi.Id,
                    UserId = wi.UserId,
                    UserName = "Mine"
                };
                wvm.Add(vm);
            }
            
            return View(wvm);
        }

        // GET: WishlistItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var wishlistItem = await _context.WishlistItem.FindAsync(id);
            _context.WishlistItem.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistItemExists(int id)
        {
            return _context.WishlistItem.Any(e => e.Id == id);
        }
    }
}
