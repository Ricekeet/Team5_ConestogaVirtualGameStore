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
    public class EventsController : Controller
    {
        private readonly CVGS_Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EventsController(CVGS_Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            return View(await _context.Event.ToListAsync());
        }
        public async Task<IActionResult> MemberIndex(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var events = from e in _context.Event select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Description.Contains(searchString));
            }
            return View(await events.ToListAsync());
        }

        public async Task<IActionResult> EnrollEvent(int id)
        {
            ViewData["Enrolled"] = "Enrolled";

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            JoinedEvent enroll = new JoinedEvent()
            {
                EventId= id,
                UserId = userId.ToString()
            };

            if (!EventUserExists(id, userId.ToString()))
            {
                _context.Add(enroll);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(MemberIndex));
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,StartDate,EndDate,Description,EventPic")] EventViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Event newEvent = new Event()
                {
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EventPic = uniqueFileName
                };

                _context.Add(newEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            EventViewModel eventViewModel = new EventViewModel()
            {
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                EventId = @event.EventId
            };

            return View(eventViewModel);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,StartDate,EndDate,Description,EventPic")] EventViewModel model)
        {
            if (id != model.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    Event newEvent = new Event()
                    {
                        EventId=model.EventId,
                        Description = model.Description,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        EventPic = uniqueFileName
                    };

                    _context.Update(newEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(model.EventId))
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
            return View(model);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }

        private bool EventUserExists(int id, string userId)
        {
            return _context.JoinedEvent.Any(e => e.EventId == id && e.UserId == userId);
        }

        private string UploadedFile(EventViewModel model)
        {
            string uniqueFileName = "";

            if (model.EventPic != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.EventPic.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.EventPic.CopyTo(fileStream);
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
