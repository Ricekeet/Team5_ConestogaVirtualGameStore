using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    // Lirjeta here
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CVGS_Context _context;

        public HomeController(ILogger<HomeController> logger, CVGS_Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var games = _context.Game.Include(g => g.Genre).Include(g => g.Platform);

            return View(await games.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
        {

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
