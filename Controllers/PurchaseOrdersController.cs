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

namespace Team5_ConestogaVirtualGameStore
{
    public class PurchaseOrdersController : Controller
    {
        private readonly CVGS_Context _context;

        public PurchaseOrdersController(CVGS_Context context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var cVGS_Context = _context.PurchaseOrder.Where(p => p.UserId == userId.ToString());
            return View(await cVGS_Context.ToListAsync());
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> CreditCard()
        {
            return View();
        }

        // 1234 5678 1234 5670
        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardHolderName,CardNumber,CardExpiryMonth,CardExpiryYear,CardCvc")] CardModel cardModel)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            PurchaseOrder purchaseOrder = new PurchaseOrder();
            purchaseOrder.DateOrdered = DateTime.Today;
            purchaseOrder.UserId = userId.ToString();
            purchaseOrder.Total = 0;

            if (ModelState.IsValid)
            {
                // create purchase order
                _context.Add(purchaseOrder);
                await _context.SaveChangesAsync();

                // get autoincrement primary key
                int nextId = purchaseOrder.OrderId;

                // create Order Items
                var cartItem = _context.CartItem.Include(c => c.Game).Where(c => c.UserId == userId.ToString()).ToList();
                foreach(CartItem cart in cartItem)
                {
                    OrderItem orderItem = new OrderItem() { 
                        GameId = cart.GameId,
                        OrderId = nextId
                    };
                    _context.Add(orderItem);
                    await _context.SaveChangesAsync();
                }
                
                // remove everything in cart 
                foreach(CartItem cart in cartItem)
                {
                    var c = await _context.CartItem.FindAsync(cart.ItemId);
                    _context.CartItem.Remove(c);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "OrderItems");
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", purchaseOrder.UserId);
            return View("CreditCard");
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrder.Any(e => e.OrderId == id);
        }
    }
}
