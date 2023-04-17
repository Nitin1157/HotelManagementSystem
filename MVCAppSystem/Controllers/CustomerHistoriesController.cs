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
    public class CustomerHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerHistories
        public async Task<IActionResult> Index()
        {
              return View(await _context.customerHistories.ToListAsync());
        }

        // GET: CustomerHistories/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.customerHistories == null)
            {
                return NotFound();
            }

            var customerHistory = await _context.customerHistories
                .FirstOrDefaultAsync(m => m.CustomerName == id);
            if (customerHistory == null)
            {
                return NotFound();
            }

            return View(customerHistory);
        }

        // GET: CustomerHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNo,RoomType,Bed,IdProof,CustomerName,EmaiId,PhoneNo,Address,City,State,PinCode,Nationality,CheckinTime,Checkout,RoomRent")] CustomerHistory customerHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerHistory);
        }

        // GET: CustomerHistories/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.customerHistories == null)
            {
                return NotFound();
            }

            var customerHistory = await _context.customerHistories.FindAsync(id);
            if (customerHistory == null)
            {
                return NotFound();
            }
            return View(customerHistory);
        }

        // POST: CustomerHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("RoomNo,RoomType,Bed,IdProof,CustomerName,EmaiId,PhoneNo,Address,City,State,PinCode,Nationality,CheckinTime,Checkout,RoomRent")] CustomerHistory customerHistory)
        {
            if (id != customerHistory.CustomerName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerHistoryExists(customerHistory.CustomerName))
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
            return View(customerHistory);
        }

        // GET: CustomerHistories/Delete/5
        public async Task<IActionResult> View(string id)
        {
            if (id == null || _context.customerHistories == null)
            {
                return NotFound();
            }

            var customerHistory = await _context.customerHistories
                .FirstOrDefaultAsync(m => m.CustomerName == id);
            if (customerHistory == null)
            {
                return NotFound();
            }

            return View(customerHistory);
        }

        // POST: CustomerHistories/Delete/5
        [HttpPost, ActionName("View")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewConfirmed(string id)
        {
            if (_context.customerHistories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.customerHistories'  is null.");
            }
            var customerHistory = await _context.customerHistories.FindAsync(id);
            if (customerHistory != null)
            {
                _context.customerHistories.Remove(customerHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerHistoryExists(string id)
        {
          return _context.customerHistories.Any(e => e.CustomerName == id);
        }
    }
}
