﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCAppSystem.DbContexts;
using MVCAppSystem.Models;


namespace MVCAppSystem.Controllers
{
    public class LoginsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginsController(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }
       

       
        // GET: Logins
        public async Task<IActionResult> Index()
        {
              return View(await _context.Logins.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .FirstOrDefaultAsync(m => m.Email == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,Role")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SignIn));
            }
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password")] Login login)
        {
            if (id != login.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Email))
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
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var login = await _context.Logins
                .FirstOrDefaultAsync(m => m.Email == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Logins == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Logins'  is null.");
            }
            var login = await _context.Logins.FindAsync(id);
            if (login != null)
            {
                _context.Logins.Remove(login);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(string id)
        {
          return _context.Logins.Any(e => e.Email == id);
        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Login log)
        {   
            if(ModelState.IsValid == true)
            {
                var credentials = _context.Logins.Where(model=> model.Email == log.Email && model.Password == log.Password && model.Role == log.Role).FirstOrDefault();
                if (credentials == null)
                {
                    ViewBag.ErrorMessage = "User Doesn't Exist";
                    return View();
                }
                else
                {
                    _contextAccessor.HttpContext.Session.SetString("email", log.Email);
                    if (log.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if(log.Role =="Staff")
                    {
                        return RedirectToAction("Index", "Staff");
                    }
                    //else
                    //{
                    //    ViewBag.ErrorMessageRole = "Please Provide Correct Role";
                    //}
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            _contextAccessor.HttpContext.Session.Clear();
            var user = _contextAccessor.HttpContext.Session.GetString("email");
            return RedirectToAction("SignIn", "Logins");
            //return Json(new { sucess = true });
        }
    }
}
