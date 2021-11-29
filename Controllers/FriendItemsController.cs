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
    public class FriendItemsController : Controller
    {
        private readonly CVGS_Context _context;

        public FriendItemsController(CVGS_Context context)
        {
            _context = context;
        }

        // GET: FriendItems
        public async Task<IActionResult> Index()
        {
            var cVGS_Context = _context.FriendItem.Include(f => f.FriendTypeNavigation);
            List<FriendItem> listFI = await cVGS_Context.ToListAsync();
            List<FriendViewModel> listFIVM = new List<FriendViewModel>();
            foreach (FriendItem fi in listFI)
            {
                var aspNetUsers = await _context.AspNetUsers
                    .FirstOrDefaultAsync(m => m.Id== fi.FriendUserId);

                FriendViewModel vm = new FriendViewModel()
                {
                    ItemId = fi.ItemId,
                    FriendType = fi.FriendType,
                    FriendTypeNavigation = fi.FriendTypeNavigation,
                    FriendUserId = fi.FriendUserId,
                    FriendUserName = aspNetUsers.Email
                };

                listFIVM.Add(vm);
            }
            return View(listFIVM);
        }

        // GET: FriendItems/Create
        public IActionResult Create()
        {
            ViewData["FriendType"] = new SelectList(_context.FriendType, "TypeId", "Name");
            return View();
        }

        // POST: FriendItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,FriendType,HostUserId,FriendUserId")] FriendItem friendItem)
        {

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.UserName.Contains(friendItem.FriendUserId));
            if (aspNetUsers == null)
            {
                ViewData["FriendType"] = new SelectList(_context.FriendType, "TypeId", "Name", friendItem.FriendType);
                return View(friendItem);
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier);
                friendItem.HostUserId = userId.ToString();
                friendItem.FriendUserId = aspNetUsers.Id;
                _context.Add(friendItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FriendType"] = new SelectList(_context.FriendType, "TypeId", "Name", friendItem.FriendType);
            return View(friendItem);
        }

        // GET: FriendItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var friendItem = await _context.FriendItem.FindAsync(id);
            _context.FriendItem.Remove(friendItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendItemExists(int id)
        {
            return _context.FriendItem.Any(e => e.ItemId == id);
        }
    }
}
