﻿using System;
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
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reports.Include(r => r.Borrowing).Include(r => r.Transaction);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var reports = await _context.Reports
                .Include(r => r.Borrowing)
                .Include(r => r.Transaction)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reports == null)
            {
                return NotFound();
            }

            return View(reports);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId");
            ViewData["TransId"] = new SelectList(_context.Transactions, "TransId", "TransId");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,TransId,BorrowingId,ReportDate")] Reports reports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", reports.BorrowingId);
            ViewData["TransId"] = new SelectList(_context.Transactions, "TransId", "TransId", reports.TransId);
            return View(reports);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var reports = await _context.Reports.FindAsync(id);
            if (reports == null)
            {
                return NotFound();
            }
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", reports.BorrowingId);
            ViewData["TransId"] = new SelectList(_context.Transactions, "TransId", "TransId", reports.TransId);
            return View(reports);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,TransId,BorrowingId,ReportDate")] Reports reports)
        {
            if (id != reports.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportsExists(reports.ReportId))
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
            ViewData["BorrowingId"] = new SelectList(_context.BookBorrowing, "BorrowingId", "BorrowingId", reports.BorrowingId);
            ViewData["TransId"] = new SelectList(_context.Transactions, "TransId", "TransId", reports.TransId);
            return View(reports);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var reports = await _context.Reports
                .Include(r => r.Borrowing)
                .Include(r => r.Transaction)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (reports == null)
            {
                return NotFound();
            }

            return View(reports);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reports == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reports'  is null.");
            }
            var reports = await _context.Reports.FindAsync(id);
            if (reports != null)
            {
                _context.Reports.Remove(reports);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportsExists(int id)
        {
          return (_context.Reports?.Any(e => e.ReportId == id)).GetValueOrDefault();
        }
    }
}
