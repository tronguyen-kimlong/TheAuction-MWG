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
    public class PostItemsController : Controller
    {
        private readonly AuctionContext _context;

        public PostItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: PostItems
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.PostItems.Include(p => p.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: PostItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postItem = await _context.PostItems
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postItem == null)
            {
                return NotFound();
            }

            return View(postItem);
        }

        // GET: PostItems/Create
        public IActionResult Create()
        {
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: PostItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUser,CategoryNum,TimesPost")] PostItem postItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", postItem.IdUser);
            return View(postItem);
        }

        // GET: PostItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postItem = await _context.PostItems.FindAsync(id);
            if (postItem == null)
            {
                return NotFound();
            }
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", postItem.IdUser);
            return View(postItem);
        }

        // POST: PostItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUser,CategoryNum,TimesPost")] PostItem postItem)
        {
            if (id != postItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostItemExists(postItem.Id))
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
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", postItem.IdUser);
            return View(postItem);
        }

        // GET: PostItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postItem = await _context.PostItems
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postItem == null)
            {
                return NotFound();
            }

            return View(postItem);
        }

        // POST: PostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postItem = await _context.PostItems.FindAsync(id);
            _context.PostItems.Remove(postItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostItemExists(int id)
        {
            return _context.PostItems.Any(e => e.Id == id);
        }
    }
}
