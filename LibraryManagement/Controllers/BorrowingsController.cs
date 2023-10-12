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
	public class BorrowingsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public BorrowingsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Borrowings
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.BookBorrowing.Include(b => b.Book).Include(b => b.Student);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Borrowings/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.BookBorrowing == null)
			{
				return NotFound();
			}

			var borrowing = await _context.BookBorrowing
				.Include(b => b.Book)
				.Include(b => b.Student)
				.FirstOrDefaultAsync(m => m.BorrowingId == id);
			if (borrowing == null)
			{
				return NotFound();
			}

			return View(borrowing);
		}

		// GET: Borrowings/Create
		public IActionResult Create()
		{
			ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BkTitle");
			ViewData["StudId"] = new SelectList(_context.Students, "StudId", "Fname");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BorrowingId,BookId,StudId,BookImg,DateBorrowed,DateReturn")] Borrowing borrowing)
		{
			if (ModelState.IsValid)
			{
				_context.Add(borrowing);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["BookId"] = new SelectList(_context.Books, "BookId", "BkTitle", borrowing.BookId);
			ViewData["StudId"] = new SelectList(_context.Students, "StudId", "Fname", borrowing.StudId);
			return View(borrowing);
		}

		// GET: Borrowings/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.BookBorrowing == null)
			{
				return NotFound();
			}

			var borrowing = await _context.BookBorrowing.FindAsync(id);
			if (borrowing == null)
			{
				return NotFound();
			}

			// Get the list of books and students for dropdowns
			ViewBag.Books = new SelectList(_context.Books, "BookId", "BkName");
			ViewBag.Students = new SelectList(_context.Students, "StudId", "Fname");

			return View(borrowing);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("BorrowingId,BookId,StudId,BookImg,DateBorrowed,DateReturn")] Borrowing borrowing)
		{
			if (id != borrowing.BorrowingId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(borrowing);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BorrowingExists(borrowing.BorrowingId))
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

			// Get the list of books and students for dropdowns
			ViewBag.Books = new SelectList(_context.Books, "BookId", "BkName", borrowing.BookId);
			ViewBag.Students = new SelectList(_context.Students, "StudId", "Fname", borrowing.StudId);

			return View(borrowing);
		}

		// GET: Borrowings/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.BookBorrowing == null)
			{
				return NotFound();
			}

			var borrowing = await _context.BookBorrowing
				.Include(b => b.Book)
				.Include(b => b.Student)
				.FirstOrDefaultAsync(m => m.BorrowingId == id);
			if (borrowing == null)
			{
				return NotFound();
			}

			return View(borrowing);
		}

		// POST: Borrowings/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.BookBorrowing == null)
			{
				return Problem("Entity set 'ApplicationDbContext.BookBorrowing'  is null.");
			}
			var borrowing = await _context.BookBorrowing.FindAsync(id);
			if (borrowing != null)
			{
				_context.BookBorrowing.Remove(borrowing);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool BorrowingExists(int id)
		{
			return (_context.BookBorrowing?.Any(e => e.BorrowingId == id)).GetValueOrDefault();
		}
	}
}
