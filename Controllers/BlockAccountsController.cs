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
    public class BlockAccountsController : Controller
    {
        private readonly AuctionContext _context;

        public BlockAccountsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: BlockAccounts
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.BlockAccounts.Include(b => b.IdManagerNavigation).Include(b => b.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: BlockAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockAccount = await _context.BlockAccounts
                .Include(b => b.IdManagerNavigation)
                .Include(b => b.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blockAccount == null)
            {
                return NotFound();
            }

            return View(blockAccount);
        }

        // GET: BlockAccounts/Create
        public IActionResult Create()
        {
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username");
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: BlockAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdManager,IdUser,Reason,Date")] BlockAccount blockAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blockAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", blockAccount.IdManager);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", blockAccount.IdUser);
            return View(blockAccount);
        }

        // GET: BlockAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockAccount = await _context.BlockAccounts.FindAsync(id);
            if (blockAccount == null)
            {
                return NotFound();
            }
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", blockAccount.IdManager);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", blockAccount.IdUser);
            return View(blockAccount);
        }

        // POST: BlockAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdManager,IdUser,Reason,Date")] BlockAccount blockAccount)
        {
            if (id != blockAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blockAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlockAccountExists(blockAccount.Id))
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
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", blockAccount.IdManager);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", blockAccount.IdUser);
            return View(blockAccount);
        }

        // GET: BlockAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blockAccount = await _context.BlockAccounts
                .Include(b => b.IdManagerNavigation)
                .Include(b => b.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blockAccount == null)
            {
                return NotFound();
            }

            return View(blockAccount);
        }

        // POST: BlockAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blockAccount = await _context.BlockAccounts.FindAsync(id);
            _context.BlockAccounts.Remove(blockAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlockAccountExists(int id)
        {
            return _context.BlockAccounts.Any(e => e.Id == id);
        }
    }
}
