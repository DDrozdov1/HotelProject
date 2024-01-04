using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppKursProject.Data;
using AppKursProject.Models;
using AppKursProject.ViewModels.Customers;

namespace AppKursProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string filterValue, string sortOrder)
        {
            ViewData["FullNameSortParam"] = sortOrder == "fullName" ? "fullName_desc" : "fullName";
            ViewData["PassportDataSortParam"] = sortOrder == "passportData" ? "passportData_desc" : "passportData";

            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(filterValue))
            {
                customers = customers.Where(c => c.FullName.Contains(filterValue) || c.PassportData.Contains(filterValue));
            }

            SortState currentSortState = SortState.FullNameAsc;

            if (!string.IsNullOrEmpty(sortOrder))
            {
                currentSortState = sortOrder switch
                {
                    "fullName_desc" => SortState.FullNameDesc,
                    "passportData" => SortState.PassportDataAsc,
                    "passportData_desc" => SortState.PassportDataDesc,
                    _ => SortState.FullNameAsc,
                };
            }

            var sortViewModel = new CustomerSortViewModel(currentSortState);

            switch (sortViewModel.Current)
            {
                case SortState.FullNameDesc:
                    customers = customers.OrderByDescending(c => c.FullName);
                    currentSortState = SortState.FullNameDesc;
                    break;
                case SortState.PassportDataAsc:
                    customers = customers.OrderBy(c => c.PassportData);
                    currentSortState = SortState.PassportDataAsc;
                    break;
                case SortState.PassportDataDesc:
                    customers = customers.OrderByDescending(c => c.PassportData);
                    currentSortState = SortState.PassportDataDesc;
                    break;
                default:
                    customers = customers.OrderBy(c => c.FullName);
                    currentSortState = SortState.FullNameAsc;
                    break;
            }

            var sortedCustomers = await customers.ToListAsync();

            ViewData["SortViewModel"] = sortViewModel;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CustomerListPartial", sortedCustomers);
            }

            return View(sortedCustomers);
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            // GET: Customers/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Customers/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("CustomerId,FullName,PassportData")] Customer customer)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }

            // GET: Customers/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }
                return View(customer);
            }

            // POST: Customers/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FullName,PassportData")] Customer customer)
            {
                if (id != customer.CustomerId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.CustomerId))
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
                return View(customer);
            }

            // GET: Customers/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Customers == null)
                {
                    return NotFound();
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(m => m.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }

                return View(customer);
            }

            // POST: Customers/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Customers == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.Customers'  is null.");
                }
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                }
            
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool CustomerExists(int id)
            {
              return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
            }
        }
    }
