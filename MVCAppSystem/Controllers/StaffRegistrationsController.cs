using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAppSystem.DbContexts;
using MVCAppSystem.Models;

namespace MVCAppSystem.Controllers
{
    public class StaffRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StaffRegistrations
        public async Task<IActionResult> Index()
        {
              return View(await _context.staffRegistrations.ToListAsync());
        }

        // GET: StaffRegistrations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.staffRegistrations == null)
            {
                return NotFound();
            }

            var staffRegistration = await _context.staffRegistrations
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (staffRegistration == null)
            {
                return NotFound();
            }

            return View(staffRegistration);
        }

        // GET: StaffRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,PhoneNumber,Email,Password")] StaffRegistration staffRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staffRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffRegistration);
        }

        // GET: StaffRegistrations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.staffRegistrations == null)
            {
                return NotFound();
            }

            var staffRegistration = await _context.staffRegistrations.FindAsync(id);
            if (staffRegistration == null)
            {
                return NotFound();
            }
            return View(staffRegistration);
        }

        // POST: StaffRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName,PhoneNumber,Email,Password")] StaffRegistration staffRegistration)
        {
            if (id != staffRegistration.UserName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffRegistrationExists(staffRegistration.UserName))
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
            return View(staffRegistration);
        }

        // GET: StaffRegistrations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.staffRegistrations == null)
            {
                return NotFound();
            }

            var staffRegistration = await _context.staffRegistrations
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (staffRegistration == null)
            {
                return NotFound();
            }

            return View(staffRegistration);
        }

        // POST: StaffRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.staffRegistrations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.staffRegistrations'  is null.");
            }
            var staffRegistration = await _context.staffRegistrations.FindAsync(id);
            if (staffRegistration != null)
            {
                _context.staffRegistrations.Remove(staffRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffRegistrationExists(string id)
        {
          return _context.staffRegistrations.Any(e => e.UserName == id);
        }
    }
}
