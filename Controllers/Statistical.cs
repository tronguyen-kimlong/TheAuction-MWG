using Auction.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Auction.Controllers
{
    [Authorize]
    public class Statistical : Controller
    {
        private readonly AuctionContext _context;

        public Statistical(AuctionContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Items() {
            //TempData["data"] = new int[] { 2, 6, 5 };
            //TempData["labels"] = new string[] { "jan", "july", "may" };

            //var query =  _context.Items

            //    .Where(w => w.IsAccept == false)
            //    .GroupBy(w => w.IdCategory)
            //    .Select(w => new {category = w.Key, count = w.Count()}).ToArray();

            //var query = from item in _context.Items
            //            join cate in _context.Categories on item.IdCategory equals cate.Id into total
            //            from x in total.DefaultIfEmpty()

            //            select new { item.IdUser };



            var query = from item in _context.Items
                        select new { item.IdUser };

            var qq = query.GroupBy(w => w.IdUser).Select(w => new {
                labels = w.Key,
                data = w.Count()
            }).ToArray();

            int[] num = new int[qq.Count()];
            string[] array = new string[qq.Count()];
            for (int j = 0; j < qq.Length; j++)
            {
                var item = qq[j];
                Console.WriteLine(item);
                array[j] = item.labels;
                num[j] = item.data;
            }


            TempData["data"] = num;
            TempData["labels"] = array;

            return View();
        }
        public IActionResult Users()
        {
            var query = from item in _context.Users
                        select new
                        {
                            item.Lockuser
                        };
            var qq = query.GroupBy(w => w.Lockuser).Select(w => new { 
                labels = w.Key, 
                data = w.Count()}).ToArray();

            int[] num = new int[qq.Count()];
            string[] array = new string[qq.Count()];
            for (int j = 0; j < qq.Length; j++)
            {
                var item = qq[j];
                Console.WriteLine(item);
                array[j] = item.labels;
                num[j] = item.data;
            }


            TempData["data"] = num;
            TempData["labels"] = array;
            return View();
        }
        private void HandleStatistical(dynamic qq)        
        {
            int[] num = new int[qq.Count()];
            string[] array = new string[qq.Count()];
            for (int j = 0; j < qq.Length; j++)
            {
                var item = qq[j];
                Console.WriteLine(item);
                array[j] = item.labels;
                num[j] = item.data;
            }


            TempData["data"] = num;
            TempData["labels"] = array;
        }
    }
}
