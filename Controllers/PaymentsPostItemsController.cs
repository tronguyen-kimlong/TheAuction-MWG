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
    [Authorize(Roles ="admin")]
    public class PaymentsPostItemsController : Controller
    {
        private readonly AuctionContext _context;

        public PaymentsPostItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: PaymentsPostItems
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.PaymentsPostItems.Include(p => p.IdPackageNavigation).Include(p => p.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: PaymentsPostItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsPostItem = await _context.PaymentsPostItems
                .Include(p => p.IdPackageNavigation)
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentsPostItem == null)
            {
                return NotFound();
            }

            return View(paymentsPostItem);
        }

        // GET: PaymentsPostItems/Create
        public IActionResult Create()
        {
            ViewData["IdPackage"] = new SelectList(_context.Packages, "Id", "Id");
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: PaymentsPostItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUser,IdPackage,Date")] PaymentsPostItem paymentsPostItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentsPostItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPackage"] = new SelectList(_context.Packages, "Id", "Id", paymentsPostItem.IdPackage);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", paymentsPostItem.IdUser);
            return View(paymentsPostItem);
        }

        // GET: PaymentsPostItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsPostItem = await _context.PaymentsPostItems.FindAsync(id);
            if (paymentsPostItem == null)
            {
                return NotFound();
            }
            ViewData["IdPackage"] = new SelectList(_context.Packages, "Id", "Id", paymentsPostItem.IdPackage);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", paymentsPostItem.IdUser);
            return View(paymentsPostItem);
        }

        // POST: PaymentsPostItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUser,IdPackage,Date")] PaymentsPostItem paymentsPostItem)
        {
            if (id != paymentsPostItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentsPostItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsPostItemExists(paymentsPostItem.Id))
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
            ViewData["IdPackage"] = new SelectList(_context.Packages, "Id", "Id", paymentsPostItem.IdPackage);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", paymentsPostItem.IdUser);
            return View(paymentsPostItem);
        }

        // GET: PaymentsPostItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsPostItem = await _context.PaymentsPostItems
                .Include(p => p.IdPackageNavigation)
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentsPostItem == null)
            {
                return NotFound();
            }

            return View(paymentsPostItem);
        }

        // POST: PaymentsPostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentsPostItem = await _context.PaymentsPostItems.FindAsync(id);
            _context.PaymentsPostItems.Remove(paymentsPostItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsPostItemExists(int id)
        {
            return _context.PaymentsPostItems.Any(e => e.Id == id);
        }
    }
}
