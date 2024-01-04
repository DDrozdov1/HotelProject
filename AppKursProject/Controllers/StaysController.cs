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
    public class StaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stays
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stays.Include(s => s.Customer).Include(s => s.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stays == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays
                .Include(s => s.Customer)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.StayId == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // GET: Stays/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: Stays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StayId,CustomerId,RoomId,CheckInDate,CheckOutDate")] Stay stay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", stay.CustomerId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", stay.RoomId);
            return View(stay);
        }

        // GET: Stays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stays == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays.FindAsync(id);
            if (stay == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", stay.CustomerId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", stay.RoomId);
            return View(stay);
        }

        // POST: Stays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StayId,CustomerId,RoomId,CheckInDate,CheckOutDate")] Stay stay)
        {
            if (id != stay.StayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StayExists(stay.StayId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", stay.CustomerId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", stay.RoomId);
            return View(stay);
        }

        // GET: Stays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stays == null)
            {
                return NotFound();
            }

            var stay = await _context.Stays
                .Include(s => s.Customer)
                .Include(s => s.Room)
                .FirstOrDefaultAsync(m => m.StayId == id);
            if (stay == null)
            {
                return NotFound();
            }

            return View(stay);
        }

        // POST: Stays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stays == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Stays'  is null.");
            }
            var stay = await _context.Stays.FindAsync(id);
            if (stay != null)
            {
                _context.Stays.Remove(stay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StayExists(int id)
        {
          return (_context.Stays?.Any(e => e.StayId == id)).GetValueOrDefault();
        }
    }
}
