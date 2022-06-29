using Auction.Data;
using Auction.Libraries.HashPassword;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    public class Account : Controller
    {
        private readonly AuctionContext _context;
        public Account(AuctionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login( string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            // get the previous Url
            ViewData["ReturnUrl"] = returnUrl;
            // find username and password in database
            var manager = await _context.Managers.FindAsync(username);
            if(manager != null) 
            {
                //decrypt password
                var hashPassword = new HashPassword();
                string decryptPass = hashPassword.DecryptString(manager.Password);
                if(password == decryptPass)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", username));
                    claims.Add(new Claim("Role", manager.Roles));
                    claims.Add(new Claim(ClaimTypes.Role, manager.Roles));
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    return Redirect(returnUrl);
                }

            }

            
            
            TempData["error"] = "username or password are wrong";
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        public IActionResult AccessDenied ()
        {
           return View();
        }
    }
}
