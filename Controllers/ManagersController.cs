using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auction.Data;
using Auction.Models;
using Auction.Libraries.HashPassword;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace Auction.Controllers
{
    [Authorize(Roles ="admin")]
    
    public class ManagersController : Controller
    {
        private readonly AuctionContext _context;

        public ManagersController(AuctionContext context)
        {
            _context = context;
        }

        // GET: Managers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Managers.ToListAsync());
        }

        // GET: Managers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // GET: Managers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Managers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,Phone,Roles")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                // encript password
                var hashPassword = new HashPassword();
                string encriptPass = hashPassword.EncryptString(manager.Password);
                manager.Password = encriptPass;
                // checking the user already in database yet!
                var alreadyUser = await _context.Managers.FirstOrDefaultAsync(
                    user => user.Username == manager.Username);
                if (alreadyUser == null)
                {
                    _context.Add(manager);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                TempData["alreadyUser"] = "The username " + manager.Username + " already exists";
                
            }
            return View(manager);
        }

        // GET: Managers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
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
        public async Task<IActionResult> Edit(string id, [Bind("Username,Password,Email,Phone,Roles")] Manager manager)
        {
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

        // GET: Managers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .FirstOrDefaultAsync(m => m.Username == id);
            if (manager == null)
            {
                return NotFound();
            }

            return View(manager);
        }

        // POST: Managers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var manager = await _context.Managers.FindAsync(id);
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public  IActionResult CreateMultiple()
        {
            return View();
        }
        [HttpPost, ActionName("CreateMultiple")]
        public async Task<IActionResult> CreateMultipleConfirmed(string headUser, int numBegin, int numEnd)
        {
            
            for(int i = numBegin; i <= numEnd; i ++)
            {
                // create new user
                string newUser = headUser + i.ToString();
                // checking the user already exits yet?
                if (!ManagerExists(newUser))
                {
                    Manager manager = new Manager();
                    manager.Username = newUser;
                    // hash password 
                    var hashPassword = new HashPassword();
                    string encryptPass = hashPassword.EncryptString(newUser);
                    manager.Password = encryptPass;
                    _context.Add(manager);
                    await _context.SaveChangesAsync();
                }
                TempData["alreadyUser"] = "The user already " + newUser + " exists ";
            }
            return View();  
        }
        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Username"),
                                        new DataColumn("Email"),
                                        new DataColumn("Phone"),
                                        new DataColumn("Password"),

                                         });

            var manager = from mn in _context.Managers
                           select mn;

            foreach (var customer in manager)
            {
                dt.Rows.Add(customer.Username, customer.Email, customer.Phone, customer.Password);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        private bool ManagerExists(string id)
        {
            return _context.Managers.Any(e => e.Username == id);
        }
       
    }
}
