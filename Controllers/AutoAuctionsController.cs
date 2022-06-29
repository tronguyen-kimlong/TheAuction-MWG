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
    public class AutoAuctionsController : Controller
    {
        private readonly AuctionContext _context;

        public AutoAuctionsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: AutoAuctions
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.AutoAuctions.Include(a => a.IdItemsNavigation).Include(a => a.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: AutoAuctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoAuction = await _context.AutoAuctions
                .Include(a => a.IdItemsNavigation)
                .Include(a => a.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autoAuction == null)
            {
                return NotFound();
            }

            return View(autoAuction);
        }

        // GET: AutoAuctions/Create
        public IActionResult Create()
        {
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser");
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: AutoAuctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdItems,IdUser,Cost,IsStillAuction,Date")] AutoAuction autoAuction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autoAuction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", autoAuction.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", autoAuction.IdUser);
            return View(autoAuction);
        }

        // GET: AutoAuctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoAuction = await _context.AutoAuctions.FindAsync(id);
            if (autoAuction == null)
            {
                return NotFound();
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", autoAuction.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", autoAuction.IdUser);
            return View(autoAuction);
        }

        // POST: AutoAuctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdItems,IdUser,Cost,IsStillAuction,Date")] AutoAuction autoAuction)
        {
            if (id != autoAuction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autoAuction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoAuctionExists(autoAuction.Id))
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
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", autoAuction.IdItems);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", autoAuction.IdUser);
            return View(autoAuction);
        }

        // GET: AutoAuctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autoAuction = await _context.AutoAuctions
                .Include(a => a.IdItemsNavigation)
                .Include(a => a.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autoAuction == null)
            {
                return NotFound();
            }

            return View(autoAuction);
        }

        // POST: AutoAuctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autoAuction = await _context.AutoAuctions.FindAsync(id);
            _context.AutoAuctions.Remove(autoAuction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoAuctionExists(int id)
        {
            return _context.AutoAuctions.Any(e => e.Id == id);
        }
    }
}
