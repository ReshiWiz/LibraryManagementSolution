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
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Borrowing).Include(t => t.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Borrowing)
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.TransId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId");
            ViewData["StudId"] = new SelectList(_context.Students, "StudId", "StudId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransId,TransName,BorrowingId,StudId,TransDate")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", transaction.BorrowingId);
            ViewData["StudId"] = new SelectList(_context.Students, "StudId", "StudId", transaction.StudId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", transaction.BorrowingId);
            ViewData["StudId"] = new SelectList(_context.Students, "StudId", "StudId", transaction.StudId);
            return View(transaction);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransId,TransName,BorrowingId,StudId,TransDate")] Transaction transaction)
        {
            if (id != transaction.TransId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransId))
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
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", transaction.BorrowingId);
            ViewData["StudId"] = new SelectList(_context.Students, "StudId", "StudId", transaction.StudId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Borrowing)
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.TransId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransId == id)).GetValueOrDefault();
        }
    }
}
