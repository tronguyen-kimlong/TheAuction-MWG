using Auction.Data;
using Auction.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Authorize]
    public class Profile : Controller
    {
        private readonly AuctionContext _context;

        public Profile(AuctionContext context) { _context = context; }
        public async Task<IActionResult> Index()
        {
            string username = getUser();
            var qq = await _context.Managers.FindAsync(username);
            if(qq != null)
            {
                return View(qq);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(string ?_id)
        {
            string id = getUser();

            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }
            return View(manager);
        }

        // POST: Managers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string ?_id, [Bind("Username,Password,Email,Phone, Roles")] Manager manager)
        {
            string id = getUser();

            if (id != manager.Username)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(manager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerExists(manager.Username))
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
            return View(manager);
        }
        private bool ManagerExists(string id)
        {
            return _context.Managers.Any(e => e.Username == id);
        }

        private string getUser()
        {
            string username = null;
            foreach (var claim in User.Claims)
            {
                if (claim.Type == "username")
                {
                    return claim.Value;
                }
            }
            return username;
        }
    }
}
