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
    public class ProvidedServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvidedServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProvidedServices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProvidedServices.Include(p => p.Employee).Include(p => p.Service).Include(p => p.Stay);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProvidedServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProvidedServices == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices
                .Include(p => p.Employee)
                .Include(p => p.Service)
                .Include(p => p.Stay)
                .FirstOrDefaultAsync(m => m.ServiceRecordId == id);
            if (providedService == null)
            {
                return NotFound();
            }

            return View(providedService);
        }

        // GET: ProvidedServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ServiceId"] = new SelectList(_context.HotelServices, "ServiceId", "ServiceId");
            ViewData["StayId"] = new SelectList(_context.Stays, "StayId", "StayId");
            return View();
        }

        // POST: ProvidedServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceRecordId,StayId,ServiceId,EmployeeId,TotalCost")] ProvidedService providedService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(providedService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", providedService.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.HotelServices, "ServiceId", "ServiceId", providedService.ServiceId);
            ViewData["StayId"] = new SelectList(_context.Stays, "StayId", "StayId", providedService.StayId);
            return View(providedService);
        }

        // GET: ProvidedServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProvidedServices == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices.FindAsync(id);
            if (providedService == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", providedService.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.HotelServices, "ServiceId", "ServiceId", providedService.ServiceId);
            ViewData["StayId"] = new SelectList(_context.Stays, "StayId", "StayId", providedService.StayId);
            return View(providedService);
        }

        // POST: ProvidedServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceRecordId,StayId,ServiceId,EmployeeId,TotalCost")] ProvidedService providedService)
        {
            if (id != providedService.ServiceRecordId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providedService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvidedServiceExists(providedService.ServiceRecordId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", providedService.EmployeeId);
            ViewData["ServiceId"] = new SelectList(_context.HotelServices, "ServiceId", "ServiceId", providedService.ServiceId);
            ViewData["StayId"] = new SelectList(_context.Stays, "StayId", "StayId", providedService.StayId);
            return View(providedService);
        }

        // GET: ProvidedServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProvidedServices == null)
            {
                return NotFound();
            }

            var providedService = await _context.ProvidedServices
                .Include(p => p.Employee)
                .Include(p => p.Service)
                .Include(p => p.Stay)
                .FirstOrDefaultAsync(m => m.ServiceRecordId == id);
            if (providedService == null)
            {
                return NotFound();
            }

            return View(providedService);
        }

        // POST: ProvidedServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProvidedServices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProvidedServices'  is null.");
            }
            var providedService = await _context.ProvidedServices.FindAsync(id);
            if (providedService != null)
            {
                _context.ProvidedServices.Remove(providedService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvidedServiceExists(int id)
        {
          return (_context.ProvidedServices?.Any(e => e.ServiceRecordId == id)).GetValueOrDefault();
        }
    }
}
