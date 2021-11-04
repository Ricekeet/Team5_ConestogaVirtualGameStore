using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult GameDetail()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddGame()
        {
            return View();
        }

        // find something by id
        // role based control do it here
    }
}
