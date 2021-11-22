using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team5_ConestogaVirtualGameStore.Models;

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var cVGS_Context = _context.WishlistItem.Include(w => w.Game).Where(w=>w.UserId==userId.ToString());
            return View(await cVGS_Context.ToListAsync());
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
