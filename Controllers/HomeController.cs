using Auction.Models;
using Auction.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{ 
    // học ttaapj theo tấm guoiwd đau đưc hò  . hơ you like thag. tí complate liy
    public class HomeController : Controller
    {
        private readonly AuctionContext _context;
        public HomeController(AuctionContext context) { _context = context; }

        public IActionResult Index()
        {
            var items = _context.Items.ToList();
            return View(items);
        }

        public IActionResult Index1()
        {
            var items = _context.Items.ToList();
            return View(items);
           
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
