using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team5_ConestogaVirtualGameStore.Models;
using Team5_ConestogaVirtualGameStore.ViewModels;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class GameController : Controller
    {
        private readonly CVGS_Context _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GameController(CVGS_Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            CartItem cartItem = new CartItem();
            cartItem.UserId = userId;
            cartItem.Quantity = 1;
            cartItem.GameId = id;

            if (ModelState.IsValid)
            {
                if (!CartItemExists(id))
                {
                    _context.Add(cartItem);
                    await _context.SaveChangesAsync();
                }
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            WishlistItem wishItem = new WishlistItem();
            wishItem.UserId = userId;
            wishItem.GameId = id;

            if (!WishItemExists(id))
            {
                _context.Add(wishItem);
                await _context.SaveChangesAsync();
            }

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
        public async Task<IActionResult> Create([Bind("GameId,GenreId,PlatformId,ReviewListId,Name,ReleaseDate,Price,Inventory,DiscountPercent,Description,GameImg")] GameViewModel gameViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(gameViewModel);

                Game newGame = new Game()
                {
                    GameId=gameViewModel.GameId,
                    GenreId = gameViewModel.GenreId,
                    PlatformId = gameViewModel.PlatformId,
                    Name = gameViewModel.Name,
                    ReleaseDate = gameViewModel.ReleaseDate,
                    Price=gameViewModel.Price,
                    Inventory = gameViewModel.Inventory,
                    DiscountPercent = gameViewModel.DiscountPercent,
                    Description=gameViewModel.Description,
                    GameImg = uniqueFileName
                };

                _context.Add(newGame);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", gameViewModel.GenreId);
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name", gameViewModel.PlatformId);
          
            return View(gameViewModel);
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

            GameViewModel gameViewModel = new GameViewModel()
            {
                GameId = game.GameId,
                GenreId = game.GenreId,
                PlatformId = game.PlatformId,
                Description = game.Description,
                Name = game.Name,
                Inventory = game.Inventory,
                Price = game.Price,
                DiscountPercent = game.DiscountPercent,
                ReleaseDate = game.ReleaseDate
            };

            return View(gameViewModel);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,GenreId,PlatformId,ReviewListId,Name,ReleaseDate,Price,Inventory,DiscountPercent,Description,GameImg")] GameViewModel gameViewModel)
        {
            if (id != gameViewModel.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(gameViewModel);

                Game newGame = new Game()
                {
                    GameId = gameViewModel.GameId,
                    GenreId = gameViewModel.GenreId,
                    PlatformId = gameViewModel.PlatformId,
                    Name = gameViewModel.Name,
                    ReleaseDate = gameViewModel.ReleaseDate,
                    Price = gameViewModel.Price,
                    Inventory = gameViewModel.Inventory,
                    DiscountPercent = gameViewModel.DiscountPercent,
                    Description = gameViewModel.Description,
                    GameImg = uniqueFileName
                };

                try
                {
                    _context.Update(newGame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(gameViewModel.GameId))
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
            ViewData["GenreId"] = new SelectList(_context.Genre, "GenreId", "Name", gameViewModel.GenreId);
            ViewData["PlatformId"] = new SelectList(_context.Platform, "PlatformId", "Name", gameViewModel.PlatformId);
            return View(gameViewModel);
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

        private bool CartItemExists(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.CartItem.Any(e => e.GameId == id && e.UserId == userId);
        }
        private bool WishItemExists(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.WishlistItem.Any(e => e.GameId == id && e.UserId == userId);
        }

        private string UploadedFile(GameViewModel model)
        {
            string uniqueFileName = null;

            if (model.GameImg != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.GameImg.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.GameImg.CopyTo(fileStream);
                }
            }

            if (uniqueFileName.Length > 49)
            {
                uniqueFileName = uniqueFileName.Substring(0, 5) + Guid.NewGuid().ToString();
            }
            return uniqueFileName;
        }

    }
}
