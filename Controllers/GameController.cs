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
    public class GameController : Controller
    {
        private readonly CVGS_Context _context;

        public GameController(CVGS_Context context)
        {
            _context = context;
        }

        // GET: Game
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var games = from g in _context.Game select g;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Name.Contains(searchString));
            }

            return View(await games.ToListAsync());
        }

        // GET: Game/AddToCart/5
        public async Task<IActionResult> AddToCart(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            CartItem cartItem = new CartItem();
            cartItem.UserId = userId.ToString();
            cartItem.Quantity = 1;
            cartItem.GameId = id;

            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Game");
        }

        // GET: Game/AddToWishlist/5
        public async Task<IActionResult> AddToWishlist(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            WishlistItem wishItem = new WishlistItem();
            wishItem.UserId = userId.ToString();
            wishItem.GameId = id;

            _context.Add(wishItem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Game");
        }


        // GET: Game/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .Include(g => g.Review)
                .FirstOrDefaultAsync(m => m.GameId == id);

            var ratings = _context.Review.Where(g => g.GameId.Equals(id.Value)).Where(r=>r.Pending == false).ToList();
            if(ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(m => m.Rating);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
            }

            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name");
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name");
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,GenreId,PlatformId,ReviewListId,Name,ReleaseDate,Price,Inventory,DiscountPercent,Description")] Game game)
        {
            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", game.GenreId);
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name", game.PlatformId);
          
            return View(game);
        }

        // GET: Game/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", game.GenreId);
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name", game.PlatformId);
            
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GenreId,PlatformId,ReviewListId,Name,ReleaseDate,Price,Inventory,DiscountPercent,Description")] Game game)
        {
            if (id != game.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", game.GenreId);
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name", game.PlatformId);
            return View(game);
        }

        // GET: Game/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Game
                .Include(g => g.Genre)
                .Include(g => g.Platform)
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Game.FindAsync(id);
            _context.Game.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Game.Any(e => e.GameId == id);
        }
    }
}
