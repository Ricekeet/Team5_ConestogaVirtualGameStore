using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team5_ConestogaVirtualGameStore.Models;

namespace Team5_ConestogaVirtualGameStore.Controllers
{
    public class AddressTypeController : Controller
    {
        private readonly CVGS_Context _context;

        public AddressTypeController(CVGS_Context context)
        {
            _context = context;
        }

        public AddressType GetAddressType(int id)
        {
            AddressType at = _context.AddressType.Find(id);
            if (at is null)
            {
                return null;
            }
            return at;
        }

        private bool AddressTypeExists(int id)
        {
            return _context.AddressType.Any(e => e.TypeId == id);
        }
    }
}
