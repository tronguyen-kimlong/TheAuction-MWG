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
//using System.Web.Script.Serialization;
namespace Auction.Controllers
{
    [Authorize(Roles ="admin")]
    public class ItemsController : Controller
    {
        private readonly AuctionContext _context;

        public ItemsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.Items.Include(i => i.IdCategoryNavigation).Include(i => i.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.IdCategoryNavigation)
                .Include(i => i.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCategory,IdUser,ImageItems,Description,Date,IsAccept,IsSold,IsPaid,Price,Discount,PriceBuyNow,Auction,PriceAuction")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Id", item.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", item.IdUser);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Id", item.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", item.IdUser);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategory,IdUser,ImageItems,Description,Date,IsAccept,IsSold,IsPaid,Price,Discount,PriceBuyNow,Auction,PriceAuction")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["IdCategory"] = new SelectList(_context.Categories, "Id", "Id", item.IdCategory);
            ViewData["IdUser"] = new SelectList(_context.Users, "Username", "Username", item.IdUser);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.IdCategoryNavigation)
                .Include(i => i.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApproveItems()
        {
            var auctionContext = _context.Items.Include(i => i.IdCategoryNavigation).Include(i => i.IdUserNavigation);
            return View(await auctionContext.ToListAsync());
        }
        public async Task<IActionResult> StatisticalCategoryItem()
        {
            //TempData["data"] = new int[] { 2, 6, 5 };
            //TempData["labels"] = new string[] { "jan", "july", "may" };

            //var query =  _context.Items

            //    .Where(w => w.IsAccept == false)
            //    .GroupBy(w => w.IdCategory)
            //    .Select(w => new {category = w.Key, count = w.Count()}).ToArray();

            var query = from item in _context.Items
                        join cate in _context.Categories on item.IdCategory equals cate.Id into total
                        from x in total.DefaultIfEmpty()

                        select new { item.IdUser };
                        
            var qq = query.GroupBy(w => w.IdUser).Select(w => new {
                iduser = w.Key, count = w.Count()}).ToArray();

            int[] num = new int[qq.Count()];
            string[] array = new string[qq.Count()];
            for(int j = 0; j < qq.Length; j++)
            {
                var item = qq[j];
                Console.WriteLine(item);
                array[j] = item.iduser;
                num[j] = item.count;
            }

            
            TempData["data"] = num;
            TempData["labels"] = array;



            return View();
        }
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
        
    }
}
