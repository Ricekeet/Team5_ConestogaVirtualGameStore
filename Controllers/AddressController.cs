using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class AddressController : Controller
    {
        private readonly CVGS_Context _context;
        public IActionResult Index()
        {
            return View();
        }

        public AddressController(CVGS_Context context)
        {
            _context = context;
        }

        public List<Address> GetAddresses(string userId)
        {
            List<Address> addresses = new List<Address>();

            var result = _context.Address.FirstOrDefault(a => a.UserId == userId);
            if (result == null)
            {
                return null;
            }

            return addresses;
        }
    }
}
