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
    public class ApprovalItemsController : Controller
    {
        private readonly AuctionContext _context;

        public ApprovalItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: ApprovalItems
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.ApprovalItems.Include(a => a.IdItemsNavigation).Include(a => a.IdManagerNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: ApprovalItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalItem = await _context.ApprovalItems
                .Include(a => a.IdItemsNavigation)
                .Include(a => a.IdManagerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalItem == null)
            {
                return NotFound();
            }

            return View(approvalItem);
        }

        // GET: ApprovalItems/Create
        public IActionResult Create()
        {
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "Id");
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username");
            return View();
        }

        // POST: ApprovalItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdManager,IdItems,Date,Reason")] ApprovalItem approvalItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(approvalItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "Id", approvalItem.IdItems);
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", approvalItem.IdManager);
            return View(approvalItem);
        }

        // GET: ApprovalItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalItem = await _context.ApprovalItems.FindAsync(id);
            if (approvalItem == null)
            {
                return NotFound();
            }
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", approvalItem.IdItems);
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", approvalItem.IdManager);
            return View(approvalItem);
        }

        // POST: ApprovalItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdManager,IdItems,Date,Reason")] ApprovalItem approvalItem)
        {
            if (id != approvalItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(approvalItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApprovalItemExists(approvalItem.Id))
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
            ViewData["IdItems"] = new SelectList(_context.Items, "Id", "IdUser", approvalItem.IdItems);
            ViewData["IdManager"] = new SelectList(_context.Managers, "Username", "Username", approvalItem.IdManager);
            return View(approvalItem);
        }

        // GET: ApprovalItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var approvalItem = await _context.ApprovalItems
                .Include(a => a.IdItemsNavigation)
                .Include(a => a.IdManagerNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (approvalItem == null)
            {
                return NotFound();
            }

            return View(approvalItem);
        }

        // POST: ApprovalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var approvalItem = await _context.ApprovalItems.FindAsync(id);
            _context.ApprovalItems.Remove(approvalItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApprovalItemExists(int id)
        {
            return _context.ApprovalItems.Any(e => e.Id == id);
        }
    }
}
