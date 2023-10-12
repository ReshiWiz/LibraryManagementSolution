using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers
{
    public class LibrariansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrariansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Librarians
        public async Task<IActionResult> Index()
        {
              return _context.Librarian != null ? 
                          View(await _context.Librarian.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Librarian'  is null.");
        }

        // GET: Librarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Librarian == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .FirstOrDefaultAsync(m => m.LibrarianId == id);
            if (librarian == null)
            {
                return NotFound();
            }

            return View(librarian);
        }

        // GET: Librarians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Librarians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibrarianId,Fname,Lname,Gender,Age,ContactAdd,UserEmail,UserPass")] Librarian librarian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(librarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(librarian);
        }

        // GET: Librarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Librarian == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian.FindAsync(id);
            if (librarian == null)
            {
                return NotFound();
            }
            return View(librarian);
        }

        // POST: Librarians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibrarianId,Fname,Lname,Gender,Age,ContactAdd,UserEmail,UserPass")] Librarian librarian)
        {
            if (id != librarian.LibrarianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(librarian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibrarianExists(librarian.LibrarianId))
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
            return View(librarian);
        }

        // GET: Librarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Librarian == null)
            {
                return NotFound();
            }

            var librarian = await _context.Librarian
                .FirstOrDefaultAsync(m => m.LibrarianId == id);
            if (librarian == null)
            {
                return NotFound();
            }

            return View(librarian);
        }

        // POST: Librarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Librarian == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Librarian'  is null.");
            }
            var librarian = await _context.Librarian.FindAsync(id);
            if (librarian != null)
            {
                _context.Librarian.Remove(librarian);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibrarianExists(int id)
        {
          return (_context.Librarian?.Any(e => e.LibrarianId == id)).GetValueOrDefault();
        }
    }
}
