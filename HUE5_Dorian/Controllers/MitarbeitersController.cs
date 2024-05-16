using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUE5_Dorian.Models;
using Microsoft.AspNetCore.Authorization;

namespace HUE5_Dorian.Controllers
{
    public class MitarbeitersController : Controller
    {
        private readonly DbContext _context;

        public MitarbeitersController(DbContext context)
        {
            _context = context;
        }

        // GET: Mitarbeiters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mitarbeiter.ToListAsync());
        }

        // GET: Mitarbeiters/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mitarbeiters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Firstname,Lastname")] Mitarbeiter mitarbeiter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mitarbeiter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }
            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Firstname,Lastname")] Mitarbeiter mitarbeiter)
        {
            if (id != mitarbeiter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mitarbeiter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MitarbeiterExists(mitarbeiter.ID))
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
            return View(mitarbeiter);
        }

        // GET: Mitarbeiters/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiter
                .FirstOrDefaultAsync(m => m.ID == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }

        // POST: Mitarbeiters/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mitarbeiter = await _context.Mitarbeiter.FindAsync(id);
            if (mitarbeiter != null)
            {
                _context.Mitarbeiter.Remove(mitarbeiter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MitarbeiterExists(int id)
        {
            return _context.Mitarbeiter.Any(e => e.ID == id);
        }
    }
}
