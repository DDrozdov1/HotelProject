using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppKursProject.Data;
using AppKursProject.Models;

namespace AppKursProject.Controllers
{
    public class RoomServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomServices.Include(r => r.Employee).Include(r => r.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomServices == null)
            {
                return NotFound();
            }

            var roomService = await _context.RoomServices
                .Include(r => r.Employee)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (roomService == null)
            {
                return NotFound();
            }

            return View(roomService);
        }

        // GET: RoomServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: RoomServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,RoomId,EmployeeId")] RoomService roomService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", roomService.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomService.RoomId);
            return View(roomService);
        }

        // GET: RoomServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomServices == null)
            {
                return NotFound();
            }

            var roomService = await _context.RoomServices.FindAsync(id);
            if (roomService == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", roomService.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomService.RoomId);
            return View(roomService);
        }

        // POST: RoomServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,RoomId,EmployeeId")] RoomService roomService)
        {
            if (id != roomService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomServiceExists(roomService.ServiceId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", roomService.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomService.RoomId);
            return View(roomService);
        }

        // GET: RoomServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomServices == null)
            {
                return NotFound();
            }

            var roomService = await _context.RoomServices
                .Include(r => r.Employee)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (roomService == null)
            {
                return NotFound();
            }

            return View(roomService);
        }

        // POST: RoomServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomServices'  is null.");
            }
            var roomService = await _context.RoomServices.FindAsync(id);
            if (roomService != null)
            {
                _context.RoomServices.Remove(roomService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomServiceExists(int id)
        {
          return (_context.RoomServices?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
