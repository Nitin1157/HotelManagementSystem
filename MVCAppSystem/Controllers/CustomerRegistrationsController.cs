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
    public class CustomerRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomerRegistrations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.customerRegisters.Include(c => c.Room);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.customerRegisters == null)
            {
                return NotFound();
            }

            var customerRegistration = await _context.customerRegisters
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.RoomNo == id);
            if (customerRegistration == null)
            {
                return NotFound();
            }

            return View(customerRegistration);
        }

        // GET: CustomerRegistrations/Create
        public IActionResult Create()
        {
            ViewData["RoomNo"] = new SelectList(_context.rooms, "RoomNo", "Bed");
            return View();
        }

        // POST: CustomerRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomNo,RoomType,Bed,IdProof,CustomerName,EmaiId,PhoneNo,Address,City,State,PinCode,Nationality,CheckinTime,Checkout,RoomRent,IsAllocated")] CustomerRegistration customerRegistration)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(customerRegistration);s
                var result = _context.Add(customerRegistration);
                if (result != null)
                {
                    var room_update = await _context.rooms.FirstOrDefaultAsync(r => r.RoomNo.Equals(customerRegistration.RoomNo));
                    if (room_update != null)
                    {
                        room_update.IsAllocated = true;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest();
                }
               
            }
            ViewData["RoomNo"] = new SelectList(_context.rooms, "RoomNo", "Bed", customerRegistration.RoomNo);
            return View(customerRegistration);
        }

        // GET: CustomerRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.customerRegisters == null)
            {
                return NotFound();
            }

            var customerRegistration = await _context.customerRegisters.FindAsync(id);
            if (customerRegistration == null)
            {
                return NotFound();
            }
            ViewData["RoomNo"] = new SelectList(_context.rooms, "RoomNo", "Bed", customerRegistration.RoomNo);
            return View(customerRegistration);
        }

        // POST: CustomerRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomNo,RoomType,Bed,IdProof,CustomerName,EmaiId,PhoneNo,Address,City,State,PinCode,Nationality,CheckinTime,Checkout,RoomRent,IsAllocated")] CustomerRegistration customerRegistration)
        {
            if (id != customerRegistration.RoomNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerRegistrationExists(customerRegistration.RoomNo))
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
            ViewData["RoomNo"] = new SelectList(_context.rooms, "RoomNo", "Bed", customerRegistration.RoomNo);
            return View(customerRegistration);
        }

        // GET: CustomerRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.customerRegisters == null)
            {
                return NotFound();
            }

            var customerRegistration = await _context.customerRegisters
                .Include(c => c.Room)
                .FirstOrDefaultAsync(m => m.RoomNo == id);
            if (customerRegistration == null)
            {
                return NotFound();
            }

            return View(customerRegistration);
        }

        // POST: CustomerRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.customerRegisters == null)
            {
                return Problem("Entity set 'ApplicationDbContext.customerRegisters'  is null.");
            }
            var customerRegistration = await _context.customerRegisters.FindAsync(id);
            if (customerRegistration != null)
            {
                CustomerHistory customerHistory = new CustomerHistory
                {
                    RoomNo = customerRegistration.RoomNo,
                    RoomType = customerRegistration.RoomType,
                    RoomRent = customerRegistration.RoomRent,
                    Address = customerRegistration.Address,
                    CustomerName = customerRegistration.CustomerName,
                    PhoneNo = customerRegistration.PhoneNo,
                    EmaiId = customerRegistration.EmaiId,
                    Bed = customerRegistration.Bed,
                    IdProof = customerRegistration.IdProof,
                    City = customerRegistration.City,
                    State = customerRegistration.State,
                    PinCode = customerRegistration.PinCode,
                    Nationality = customerRegistration.Nationality,
                    CheckinTime = customerRegistration.CheckinTime,
                    Checkout = DateTime.Now,
                };
                _context.Add(customerHistory);
                _context.customerRegisters.Remove(customerRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
                return View(customerRegistration);
            }
        
        private bool CustomerRegistrationExists(int id)
        {
            return _context.customerRegisters.Any(e => e.RoomNo == id);
        }
    }
        
    }

