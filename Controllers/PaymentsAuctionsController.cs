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
    public class PaymentsAuctionsController : Controller
    {
        private readonly AuctionContext _context;

        public PaymentsAuctionsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: PaymentsAuctions
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.PaymentsAuctions.Include(p => p.IdUserBuyerNavigation).Include(p => p.IdUserSellerNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: PaymentsAuctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsAuction = await _context.PaymentsAuctions
                .Include(p => p.IdUserBuyerNavigation)
                .Include(p => p.IdUserSellerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentsAuction == null)
            {
                return NotFound();
            }

            return View(paymentsAuction);
        }

        // GET: PaymentsAuctions/Create
        public IActionResult Create()
        {
            ViewData["IdUserBuyer"] = new SelectList(_context.Users, "Username", "Username");
            ViewData["IdUserSeller"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: PaymentsAuctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUserBuyer,IdUserSeller,AccountBankBuyer,AccountBankSeller,Cost,Discount,Date")] PaymentsAuction paymentsAuction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentsAuction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUserBuyer"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserBuyer);
            ViewData["IdUserSeller"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserSeller);
            return View(paymentsAuction);
        }

        // GET: PaymentsAuctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsAuction = await _context.PaymentsAuctions.FindAsync(id);
            if (paymentsAuction == null)
            {
                return NotFound();
            }
            ViewData["IdUserBuyer"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserBuyer);
            ViewData["IdUserSeller"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserSeller);
            return View(paymentsAuction);
        }

        // POST: PaymentsAuctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUserBuyer,IdUserSeller,AccountBankBuyer,AccountBankSeller,Cost,Discount,Date")] PaymentsAuction paymentsAuction)
        {
            if (id != paymentsAuction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentsAuction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsAuctionExists(paymentsAuction.Id))
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
            ViewData["IdUserBuyer"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserBuyer);
            ViewData["IdUserSeller"] = new SelectList(_context.Users, "Username", "Username", paymentsAuction.IdUserSeller);
            return View(paymentsAuction);
        }

        // GET: PaymentsAuctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentsAuction = await _context.PaymentsAuctions
                .Include(p => p.IdUserBuyerNavigation)
                .Include(p => p.IdUserSellerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentsAuction == null)
            {
                return NotFound();
            }

            return View(paymentsAuction);
        }

        // POST: PaymentsAuctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentsAuction = await _context.PaymentsAuctions.FindAsync(id);
            _context.PaymentsAuctions.Remove(paymentsAuction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsAuctionExists(int id)
        {
            return _context.PaymentsAuctions.Any(e => e.Id == id);
        }
    }
}
