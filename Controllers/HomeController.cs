using Karnel_Travels.Data;
using Karnel_Travels.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;

namespace Karnel_Travels.Controllers
{
    public class HomeController : Controller
    {
        private readonly KarnelTravelContext _db;

        public HomeController(KarnelTravelContext db)
        {
            _db = db;
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
                _db.Add(user);
                _db.SaveChanges();
                TempData["Message"] = "User Registered Successfully..";
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        // Login Action Method
        public IActionResult Login(User user)
        {
            var data = _db.Users.FirstOrDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);
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
                        new Claim(ClaimTypes.Role, "Admin")
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
                        new Claim(ClaimTypes.Role, "User")
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

        // Logout Action Method
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        // Index Action Method
        public IActionResult Index()
        {
            return View();
        }

        // About Action Method
        public IActionResult About()
        {
            return View();
        }

        // Destination Action Method
        public IActionResult Destination()
        {
            return View();
        }

        // Hotel Action Method
        public IActionResult Hotel()
        {
            var hotels = _db.Hotels.ToList();
            return View(hotels);
        }
        
        [HttpGet]
        public IActionResult FetchHotels(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var hotels = string.IsNullOrEmpty(searchText) ? _db.Hotels.ToList() : _db.Hotels.Where(h => h.HotelName.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_HotelCards", hotels);
            return View();
        }

        public IActionResult Tourist()
        {
            var Tourists = _db.TouristSpots.ToList();
            return View(Tourists);
        }

        [HttpGet]
        public IActionResult FetchTourist(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var Tourists = string.IsNullOrEmpty(searchText) ? _db.TouristSpots.ToList() : _db.TouristSpots.Where(h => h.SpotName.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_TouristCards", Tourists);
            return View();
        }

        public IActionResult Travel()
        {
            var Travels = _db.Travels.ToList();
            return View(Travels);
        }

        [HttpGet]
        public IActionResult FetchTravel(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var Travels = string.IsNullOrEmpty(searchText) ? _db.Travels.ToList() : _db.Travels.Where(h => h.TravelMode.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_TravelCards", Travels);
            return View();
        }


        public IActionResult Restaurant()
        {
            var Restaurants = _db.Restaurants.ToList();
            return View(Restaurants);
        }

        [HttpGet]
        public IActionResult FetchRestaurants(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var Restaurants = string.IsNullOrEmpty(searchText) ? _db.Restaurants.ToList() : _db.Restaurants.Where(h => h.RestaurantName.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_RestaurantCards", Restaurants);
            return View();
        }



        public IActionResult Resorts()
        {
            var Resorts = _db.Resorts.ToList();
            return View(Resorts);
        }

        [HttpGet]
        public IActionResult FetchResorts(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var Resorts = string.IsNullOrEmpty(searchText) ? _db.Resorts.ToList() : _db.Resorts.Where(h => h.ResortName.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_ResortsCards", Resorts);
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }

        // Other action methods for resorts, restaurants, travel, tourist, blog, contact, privacy, error, etc.

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
