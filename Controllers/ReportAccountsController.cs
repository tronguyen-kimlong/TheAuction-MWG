using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auction.Data;
using Auction.Models;
using Microsoft.AspNetCore.Authorization;

namespace Auction.Controllers
{
    [Authorize]
    public class ReportAccountsController : Controller
    {
        private readonly AuctionContext _context;

        public ReportAccountsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: ReportAccounts
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.ReportAccounts.Include(r => r.IdItemsNavigation).Include(r => r.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: ReportAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccounts
                .Include(r => r.IdItemsNavigation)
                .Include(r => r.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportAccount == null)
            {
                return NotFound();
            }

            return View(reportAccount);
        }

        // GET: ReportAccounts/Create
        public IActionResult Create()
        {
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser");
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: ReportAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUser,IdItems,Date,Reason")] ReportAccount reportAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", reportAccount.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", reportAccount.IdUser);
            return View(reportAccount);
        }

        // GET: ReportAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccounts.FindAsync(id);
            if (reportAccount == null)
            {
                return NotFound();
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", reportAccount.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", reportAccount.IdUser);
            return View(reportAccount);
        }

        // POST: ReportAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUser,IdItems,Date,Reason")] ReportAccount reportAccount)
        {
            if (id != reportAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportAccountExists(reportAccount.Id))
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
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", reportAccount.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", reportAccount.IdUser);
            return View(reportAccount);
        }

        // GET: ReportAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportAccount = await _context.ReportAccounts
                .Include(r => r.IdItemsNavigation)
                .Include(r => r.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportAccount == null)
            {
                return NotFound();
            }

            return View(reportAccount);
        }

        // POST: ReportAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportAccount = await _context.ReportAccounts.FindAsync(id);
            _context.ReportAccounts.Remove(reportAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportAccountExists(int id)
        {
            return _context.ReportAccounts.Any(e => e.Id == id);
        }
    }
}
