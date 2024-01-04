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
    public class HotelServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HotelServices
        public async Task<IActionResult> Index()
        {
              return _context.HotelServices != null ? 
                          View(await _context.HotelServices.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HotelServices'  is null.");
        }

        // GET: HotelServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HotelServices == null)
            {
                return NotFound();
            }

            var hotelService = await _context.HotelServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (hotelService == null)
            {
                return NotFound();
            }

            return View(hotelService);
        }

        // GET: HotelServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HotelServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,Description,Price")] HotelService hotelService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotelService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotelService);
        }

        // GET: HotelServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HotelServices == null)
            {
                return NotFound();
            }

            var hotelService = await _context.HotelServices.FindAsync(id);
            if (hotelService == null)
            {
                return NotFound();
            }
            return View(hotelService);
        }

        // POST: HotelServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,Description,Price")] HotelService hotelService)
        {
            if (id != hotelService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotelService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelServiceExists(hotelService.ServiceId))
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
            return View(hotelService);
        }

        // GET: HotelServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HotelServices == null)
            {
                return NotFound();
            }

            var hotelService = await _context.HotelServices
                .FirstOrDefaultAsync(m => m.ServiceId == id);
            if (hotelService == null)
            {
                return NotFound();
            }

            return View(hotelService);
        }

        // POST: HotelServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HotelServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HotelServices'  is null.");
            }
            var hotelService = await _context.HotelServices.FindAsync(id);
            if (hotelService != null)
            {
                _context.HotelServices.Remove(hotelService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelServiceExists(int id)
        {
          return (_context.HotelServices?.Any(e => e.ServiceId == id)).GetValueOrDefault();
        }
    }
}
