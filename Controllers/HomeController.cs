using Karnel_Travels.Data;
using Karnel_Travels.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Karnel_Travels.Controllers
{
    public class HomeController : Controller
    {
        private readonly KarnelTravelContext db;

        public HomeController(KarnelTravelContext db)
        {
            this.db = db;
        }
        // Signup View
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // Signup Action Method
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserRoleId = 2;
                db.Add(user);
                db.SaveChanges();
                TempData["Message"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }



        // Login Action Method
        public IActionResult Login(User User)
        {
            var data = db.Users.FirstOrDefault(x => x.UserEmail == User.UserEmail && x.UserPassword == User.UserPassword);
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (data != null)
            {

                if (data.UserRoleId == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
               new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"Admin")
           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                else
                {
                    identity = new ClaimsIdentity(new[]
                 {
              new Claim(ClaimTypes.Name, data.UserName),
               new Claim(ClaimTypes.Email, data.UserEmail),
                new Claim(ClaimTypes.NameIdentifier, data.UserId.ToString()),
               new Claim(ClaimTypes.Role,"User")

           }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (data.UserRoleId == 1)
                    {
                        return RedirectToAction("AdminIndex", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Destination()
        {
            return View();
        }
        public ActionResult Hotel(string searchString)
        {
            var hotels = from h in db.Hotels select h;

            if (!string.IsNullOrEmpty(searchString))
            {
                hotels = hotels.Where(h => h.HotelName.Contains(searchString));
            }

            return View(hotels.ToList());
        }
        public IActionResult Resorts()
        {
            return View();
        }
        public IActionResult Restarurant()
        {
            return View();
        }
        public IActionResult Travel()
        {
            return View();
        }
        public IActionResult Tourist()
        {
            return View();
        } 
+



        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
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