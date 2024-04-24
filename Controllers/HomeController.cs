using Karnel_Travels.Data;
using Karnel_Travels.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Net.Mail;
using System.Net;

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
            //var p = _db.Packages.ToList();
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
            var travels = _db.Travels.ToList();
            return View(travels);
        }

        [HttpGet]
        public IActionResult FetchTravel(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var t = string.IsNullOrEmpty(searchText) ? _db.Travels.ToList() : _db.Travels.Where(h => h.TravelMode.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_TravelCards",t);
            return View();
        }


        public IActionResult Restaurant()
        {
            var Restaurants = _db.Restaurants.ToList();
            return View(Restaurants);
        }

        [HttpGet]
        public IActionResult FetchRestaurant(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var Restaurants = string.IsNullOrEmpty(searchText) ? _db.Restaurants.ToList() : _db.Restaurants.Where(y => y.RestaurantName.Contains(searchText)).ToList();

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
            return PartialView("_ResortCards", Resorts);
            return View();
        }

        public IActionResult Package()
        {
            var packages = _db.Packages.ToList();
            return View(packages);
        }

        [HttpGet]
        public IActionResult FetchPackage(string searchText)
        {
            // Fetch all hotels if search text is empty or null
            var packagesa = string.IsNullOrEmpty(searchText) ? _db.Packages.ToList():_db.Packages.Where(h => h.PackageName.Contains(searchText)).ToList();

            // Return partial view with hotel cards
            return PartialView("_PackageCards", packagesa);
           
        }

        [HttpGet]
        public IActionResult Details(string table, int id)
{
    switch(table.ToLower())
    {
        case "hotel":
            var hotel = _db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View("HotelDetails", hotel);
        case "travel":
            var travel = _db.Travels.Find(id);
            if (travel == null)
            {
                return NotFound();
            }
            return View("TravelDetails", travel);
            
        case "resorts":
            var resorts = _db.Resorts.Find(id);
            if (resorts == null)
            {
                return NotFound();
            }
            return View("ResortDetails", resorts);
            
        case "restaurant":
            var restaurant = _db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View("RestaurantDetails", restaurant);
            
        case "touristspot":
            var touristspot = _db.TouristSpots.Find(id);
            if (touristspot == null)
            {
                return NotFound();
            }
            return View("TouristSpotDetails", touristspot);

        case "Package2":
            var Package = _db.Packages.Find(id);
            if (Package == null)
            {
                return NotFound();
            }
            return View("PackageDetails", Package);
             default:
            return NotFound();
    }
}
        [Authorize(Roles = "User,Admin")]
        public IActionResult Feedback(int? id)
        {
            // Fetch the data based on the provided ID
            var data = _db.Feedbacks.FirstOrDefault(x => x.FeedbackId == id);

            // Initialize SelectList items
            ViewBag.Travel = new SelectList(_db.Travels, "TravelId", "TravelMode");
            ViewBag.TouristSpot = new SelectList(_db.TouristSpots, "SpotId", "SpotName");
            ViewBag.Restaurant = new SelectList(_db.Restaurants, "RestaurantId", "RestaurantName");
            ViewBag.Resort = new SelectList(_db.Resorts, "ResortId", "ResortName");
            ViewBag.Hotel = new SelectList(_db.Hotels, "HotelId", "HotelName");

            var selectListItems = new List<SelectListItem>();

            // Add items from ViewBag properties with IDs if their ID matches the provided ID
            if (id != null)
            {
                selectListItems.AddRange(((IEnumerable<SelectListItem>)ViewBag.Travel).Where(item => Convert.ToInt32(item.Value) == id));
                selectListItems.AddRange(((IEnumerable<SelectListItem>)ViewBag.TouristSpot).Where(item => Convert.ToInt32(item.Value) == id));
                selectListItems.AddRange(((IEnumerable<SelectListItem>)ViewBag.Restaurant).Where(item => Convert.ToInt32(item.Value) == id));
                selectListItems.AddRange(((IEnumerable<SelectListItem>)ViewBag.Resort).Where(item => Convert.ToInt32(item.Value) == id));
                selectListItems.AddRange(((IEnumerable<SelectListItem>)ViewBag.Hotel).Where(item => Convert.ToInt32(item.Value) == id));
            }

            // Assign the filtered list to ViewBag
            ViewBag.SelectListItems = selectListItems;

            return View(data);
        }



        public IActionResult Feedback2(Feedback feed)
        {
                    _db.Add(feed);
                    _db.SaveChanges();

			SmtpClient client = new SmtpClient("hamzasiddiqui1317@gmail.com", 0566);
			client.EnableSsl = true;
			client.UseDefaultCredentials = false;
			client.Credentials = new NetworkCredential("hamzasiddiqui1317@gmail.com", "0566 607 519"); //From 

			MailMessage Msg = new MailMessage("hamzasiddiqui1317@gmail.com" , feed.FeedbackMassage); // To

			Msg.Subject = "Your Booking Request have Been Sumited";
			Msg.Body = feed.FeedbackUserName + "Thankx for Booking us!" + "\n" + feed.FeedbackMassage;
			client.Send(Msg);

			TempData["ContMessage"] = "Successfully Submit";
			return RedirectToAction(nameof(Index));
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
