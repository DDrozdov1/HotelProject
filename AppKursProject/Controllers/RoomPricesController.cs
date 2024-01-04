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
    public class RoomPricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoomPrices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoomPrices.Include(r => r.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoomPrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomPrices == null)
            {
                return NotFound();
            }

            var roomPrice = await _context.RoomPrices
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (roomPrice == null)
            {
                return NotFound();
            }

            return View(roomPrice);
        }

        // GET: RoomPrices/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: RoomPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PriceId,Price,RoomId")] RoomPrice roomPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomPrice.RoomId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomPrices == null)
            {
                return NotFound();
            }

            var roomPrice = await _context.RoomPrices.FindAsync(id);
            if (roomPrice == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomPrice.RoomId);
            return View(roomPrice);
        }

        // POST: RoomPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PriceId,Price,RoomId")] RoomPrice roomPrice)
        {
            if (id != roomPrice.PriceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomPriceExists(roomPrice.PriceId))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", roomPrice.RoomId);
            return View(roomPrice);
        }

        // GET: RoomPrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomPrices == null)
            {
                return NotFound();
            }

            var roomPrice = await _context.RoomPrices
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.PriceId == id);
            if (roomPrice == null)
            {
                return NotFound();
            }

            return View(roomPrice);
        }

        // POST: RoomPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RoomPrices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RoomPrices'  is null.");
            }
            var roomPrice = await _context.RoomPrices.FindAsync(id);
            if (roomPrice != null)
            {
                _context.RoomPrices.Remove(roomPrice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomPriceExists(int id)
        {
          return (_context.RoomPrices?.Any(e => e.PriceId == id)).GetValueOrDefault();
        }
    }
}
