using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team5_ConestogaVirtualGameStore.Models;


namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class OrderItemsController : Controller
    {
        private readonly CVGS_Context _context;

        public OrderItemsController(CVGS_Context context)
        {
            _context = context;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var cVGS_Context = _context.OrderItem.Include(o => o.Game).Include(o => o.Order);
            return View(await cVGS_Context.ToListAsync());
        }


        public async Task<ActionResult> SendHardCopy(int id)
        {
            Game game = _context.Game.Where(g => g.GameId == id).FirstOrDefault();
            if (game.Inventory > 0) { game.Inventory -= 1; }


            OrderItem oi = _context.OrderItem.Where(oi=>oi.ItemId == id).FirstOrDefault();
            oi.Status = "Shipping";
            _context.Update(oi);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Download(int id)
        {
            Game game = _context.Game.Where(g => g.GameId == id).ToList()[0];
            string content = game.GameId + game.Description + game.Genre?.Name + game.Platform?.Name;
            string title = game.Name + ".txt";

            return File(Encoding.UTF8.GetBytes(content), "text/plain", title);
        }
    }
}
