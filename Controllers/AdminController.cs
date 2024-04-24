using Karnel_Travels.Data;
using Karnel_Travels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Karnel_Travels.Controllers
{
    public class AdminController : Controller
    {
        private readonly KarnelTravelContext db;

        public AdminController(KarnelTravelContext db)
        {
            this.db = db;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            int totalCount = db.Hotels.Count();
            int totalCount1 = db.Travels.Count();
            int totalCount2 = db.Restaurants.Count();
            int totalCount3 = db.Resorts.Count();
            int totalCount4 = db.TouristSpots.Count();
            int totalCount5 = db.Packages.Count();
            int totalCount6 = db.Users.Count();
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalCount1 = totalCount1;
            ViewBag.TotalCount2 = totalCount2;
            ViewBag.TotalCount3 = totalCount3;
            ViewBag.TotalCount4 = totalCount4;
            ViewBag.TotalCount5 = totalCount5;
            ViewBag.TotalCount5 = totalCount6;
            return View();
        }



        // Tourist Spot Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult TouristSpot()
        {
            return View();
        }

        // Image Insertion in Tourist Spot
        [HttpPost]
        public IActionResult TouristSpot(TouristSpot spot, IFormFile SpotImage)
        {

            if (SpotImage != null && SpotImage.Length > 0)
            {
                var filename = Path.GetFileName(SpotImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/TouristSpot", filename);
                var dbpath = Path.Combine("assets/img/TouristSpot", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    SpotImage.CopyTo(stream);
                }
                spot.SpotImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(spot);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchTouristSpot));
                }
            }
            else
            {
                ModelState.AddModelError("SpotImage", "Spot Image field is Required");
            }
            return View();
        }

        // Fetch Tourist Spot 
        [Authorize(Roles = "Admin")]
        public IActionResult FetchTouristSpot()
        {

            return View(db.TouristSpots.ToList());
        }

        // Delete Tourist Spot 
        public IActionResult DeleteSpot(int? id)
        {
            var data = db.TouristSpots.FirstOrDefault(x => x.SpotId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchTouristSpot));
        }

        // Update Tourist Spot view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateSpot(int? id)
        {
            var data = db.TouristSpots.FirstOrDefault(x => x.SpotId == id);
            return View(data);
        }

        // Update Tourist Spot action method
        [HttpPost]
        public IActionResult UpdateSpot(TouristSpot UpdateSpot2, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/TouristSpot");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/TouristSpot", unique_name);
                UpdateSpot2.SpotImage = dbPath;
                db.Update(UpdateSpot2);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchTouristSpot));
            }
            else
            {
                var existingSpot = db.TouristSpots.FirstOrDefault(p => p.SpotId == UpdateSpot2.SpotId);
                if (existingSpot != null)
                {
                    UpdateSpot2.SpotImage = existingSpot.SpotImage;

                    // Detach existing tracked entity
                    db.Entry(existingSpot).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchTouristSpot));
                }
            }

            // Update entity state
            db.Update(UpdateSpot2);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchTouristSpot));
        }






        // Travel Form view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Travel()
        {
            return View();
        }
        // Travel Image Insertion
        [HttpPost]
        public IActionResult Travel(Travel Travel1, IFormFile TravelImage)
        {

            if (TravelImage != null && TravelImage.Length > 0)
            {
                var filename = Path.GetFileName(TravelImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/Travel", filename);
                var dbpath = Path.Combine("assets/img/Travel", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    TravelImage.CopyTo(stream);
                }
                Travel1.TravelImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Travel1);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchTravel));
                }
            }
            else
            {
                ModelState.AddModelError("TravelImage", "Travel Image field is Required");
            }
            return View();
        }

        // Fetch Travel
        [Authorize(Roles = "Admin")]
        public IActionResult FetchTravel()
        {

            return View(db.Travels.ToList());
        }

        // Delete Travel 
        public IActionResult DeleteTravel(int? id)
        {
            var data = db.Travels.FirstOrDefault(x => x.TravelId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchTravel));
        }

        // Update Travel view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateTravel(int? id)
        {
            var data = db.Travels.FirstOrDefault(x => x.TravelId == id);
            return View(data);
        }

        // Update Travel action method
        [HttpPost]
        public IActionResult UpdateTravel(Travel UpdateTravel, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/Travel");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/Travel", unique_name);
                UpdateTravel.TravelImage = dbPath;
                db.Update(UpdateTravel);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchTravel));
            }
            else
            {
                var existingTravel = db.Travels.FirstOrDefault(p => p.TravelId == UpdateTravel.TravelId);
                if (existingTravel != null)
                {
                    UpdateTravel.TravelImage = existingTravel.TravelImage;

                    // Detach existing tracked entity
                    db.Entry(existingTravel).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchTravel));
                }
            }

            // Update entity state
            db.Update(UpdateTravel);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchTravel));
        }






        // Hotel Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Hotel()
        {
            return View();
        }

        // Image Insertion in Hotel
        [HttpPost]
        public IActionResult Hotel(Hotel Hotel1, IFormFile HotelImage)
        {

            if (HotelImage != null && HotelImage.Length > 0)
            {
                var filename = Path.GetFileName(HotelImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/Hotel", filename);
                var dbpath = Path.Combine("assets/img/Hotel", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    HotelImage.CopyTo(stream);
                }
                Hotel1.HotelImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Hotel1);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchHotel));
                }
            }
            else
            {
                ModelState.AddModelError("HotelImage", "Hotel Image field is Required");
            }
            return View();
        }

        // Fetch Hotel
        [Authorize(Roles = "Admin")]
        public IActionResult FetchHotel()
        {

            return View(db.Hotels.ToList());
        }

        // Delete Hotel 
        public IActionResult DeleteHotel(int? id)
        {
            var data = db.Hotels.FirstOrDefault(x => x.HotelId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchHotel));
        }

        // Update Hotel view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateHotel(int? id)
        {
            var data = db.Hotels.FirstOrDefault(x => x.HotelId == id);
            return View(data);
        }

        // Update Hotel action method
        [HttpPost]
        public IActionResult UpdateHotel(Hotel UpdateHotel, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/Hotel");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/Hotel", unique_name);
                UpdateHotel.HotelImage = dbPath;
                db.Update(UpdateHotel);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchHotel));
            }
            else
            {
                var existingHotel = db.Hotels.FirstOrDefault(p => p.HotelId == UpdateHotel.HotelId);
                if (existingHotel != null)
                {
                    UpdateHotel.HotelImage = existingHotel.HotelImage;

                    // Detach existing tracked entity
                    db.Entry(existingHotel).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchHotel));
                }
            }

            // Update entity state
            db.Update(UpdateHotel);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchHotel));
        }







        // Restaurant Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Restaurant()
        {
            return View();
        }

        // Image Insertion in Restaurant
        [HttpPost]
        public IActionResult Restaurant(Restaurant Restaurant1, IFormFile RestaurantImage)
        {

            if (RestaurantImage != null && RestaurantImage.Length > 0)
            {
                var filename = Path.GetFileName(RestaurantImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/Restaurant", filename);
                var dbpath = Path.Combine("assets/img/Restaurant", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    RestaurantImage.CopyTo(stream);
                }
                Restaurant1.RestaurantImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Restaurant1);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchRestaurant));
                }
            }
            else
            {
                ModelState.AddModelError("RestaurantImage", "Restaurant Image field is Required");
            }
            return View();
        }

        // Fetch Restaurant
        [Authorize(Roles = "Admin")]
        public IActionResult FetchRestaurant()
        {
            return View(db.Restaurants.ToList());
        }

        // Delete Restaurant 
        public IActionResult DeleteRestaurant(int? id)
        {
            var data = db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchRestaurant));
        }

        // Update Restaurant view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateRestaurant(int? id)
        {
            var data = db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
            return View(data);
        }

        // Update Restaurant action method
        [HttpPost]
        public IActionResult UpdateRestaurant(Restaurant UpdateRestaurant, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/Restaurant");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/Restaurant", unique_name);
                UpdateRestaurant.RestaurantImage = dbPath;
                db.Update(UpdateRestaurant);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchRestaurant));
            }
            else
            {
                var existingRestaurant = db.Restaurants.FirstOrDefault(p => p.RestaurantId == UpdateRestaurant.RestaurantId);
                if (existingRestaurant != null)
                {
                    UpdateRestaurant.RestaurantImage = existingRestaurant.RestaurantImage;

                    // Detach existing tracked entity
                    db.Entry(existingRestaurant).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchRestaurant));
                }
            }

            // Update entity state
            db.Update(UpdateRestaurant);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchRestaurant));
        }






        // Resort Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Resort()
        {
            return View();
        }

        // Image Insertion in Resort
        [HttpPost]
        public IActionResult Resort(Resort Resort1, IFormFile ResortImage)
        {

            if (ResortImage != null && ResortImage.Length > 0)
            {
                var filename = Path.GetFileName(ResortImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/Resort", filename);
                var dbpath = Path.Combine("assets/img/Resort", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    ResortImage.CopyTo(stream);
                }
                Resort1.ResortImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Resort1);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchResort));
                }
            }
            else
            {
                ModelState.AddModelError("ResortImage", "Resort Image field is Required");
            }
            return View();
        }

        // Fetch Resort
        [Authorize(Roles = "Admin")]
        public IActionResult FetchResort()
        {

            return View(db.Resorts.ToList());
        }

        // Delete Resort 
        public IActionResult DeleteResort(int? id)
        {
            var data = db.Resorts.FirstOrDefault(x => x.ResortId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchResort));
        }

        // Update Resort view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdateResort(int? id)
        {
            var data = db.Resorts.FirstOrDefault(x => x.ResortId == id);
            return View(data);
        }

        // Update Resort action method
        [HttpPost]
        public IActionResult UpdateResort(Resort UpdateResort, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/Resort");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/Resort", unique_name);
                UpdateResort.ResortImage = dbPath;
                db.Update(UpdateResort);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchResort));
            }
            else
            {
                var existingResort = db.Resorts.FirstOrDefault(p => p.ResortId == UpdateResort.ResortId);
                if (existingResort != null)
                {
                    UpdateResort.ResortImage = existingResort.ResortImage;

                    // Detach existing tracked entity
                    db.Entry(existingResort).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchResort));
                }
            }

            // Update entity state
            db.Update(UpdateResort);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchResort));
        }






        // Package Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Package()
        {
            ViewBag.Travel = new SelectList(db.Travels, "TravelId", "TravelMode");
            ViewBag.TouristSpot = new SelectList(db.TouristSpots, "SpotId", "SpotName");
            ViewBag.Restaurant = new SelectList(db.Restaurants, "RestaurantId", "RestaurantName");
            ViewBag.Resort = new SelectList(db.Resorts, "ResortId", "ResortName");
            ViewBag.Hotel = new SelectList(db.Hotels, "HotelId", "HotelName");
            return View();
        }

        // Image Insertion in Package
        [HttpPost]
        public IActionResult Package(Package Package1, IFormFile PackageImage)
        {

            if (PackageImage != null && PackageImage.Length > 0)
            {
                var filename = Path.GetFileName(PackageImage.FileName);
                string folderPath = Path.Combine("wwwroot/assets/img/Package", filename);
                var dbpath = Path.Combine("assets/img/Package", filename);
                using (var stream = new FileStream(folderPath, FileMode.Create))
                {
                    PackageImage.CopyTo(stream);
                }
                Package1.PackageImage = dbpath;

                if (ModelState.IsValid) // validation
                {
                    db.Add(Package1);
                    db.SaveChanges();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction(nameof(FetchPackage));
                }
            }
            else
            {
                ModelState.AddModelError("PackageImage", "Package Image field is Required");
            }
            return View();
        }

        // Fetch Package
        [Authorize(Roles = "Admin")]
        public IActionResult FetchPackage()
        {
            return View(db.Packages
                .Include("PackageHotel")
                .Include("PackageTravel")
                .Include("PackageTouristSpot")
                .Include("PackageRestaurant")
                .Include("PackageResort")
                .ToList());
        }

        // Delete Package 
        public IActionResult DeletePackage(int? id)
        {
            var data = db.Packages.FirstOrDefault(x => x.PackageId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPackage));
        }

        // Update Package view
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UpdatePackage(int? id)
        {
            var data = db.Packages.FirstOrDefault(x => x.PackageId == id);
            ViewBag.Travel = new SelectList(db.Travels, "TravelId", "TravelMode");
            ViewBag.TouristSpot = new SelectList(db.TouristSpots, "SpotId", "SpotName");
            ViewBag.Restaurant = new SelectList(db.Restaurants, "RestaurantId", "RestaurantName");
            ViewBag.Resort = new SelectList(db.Resorts, "ResortId", "ResortName");
            ViewBag.Hotel = new SelectList(db.Hotels, "HotelId", "HotelName");
            return View(data);
        }
      
        // Update Package action method
        [HttpPost]
        public IActionResult UpdatePackage(Package UpdatePackage, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                Guid r = Guid.NewGuid();
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extensionn = file.ContentType.ToLower();
                var exten_presize = extensionn.Substring(6);

                var unique_name = fileName + r + "." + exten_presize;
                string imagesFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/assets/img/Package");
                string filePath = Path.Combine(imagesFolder, unique_name);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbPath = Path.Combine("assets/img/Package", unique_name);
                UpdatePackage.PackageImage = dbPath;
                db.Update(UpdatePackage);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchPackage));
            }
            else
            {
                var existingPackage = db.Packages.FirstOrDefault(p => p.PackageId == UpdatePackage.PackageId);
                if (existingPackage != null)
                {
                    UpdatePackage.PackageImage = existingPackage.PackageImage;

                    // Detach existing tracked entity
                    db.Entry(existingPackage).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchPackage));
                }
            }

            // Update entity state
            db.Update(UpdatePackage);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchPackage));
        }
    }
}
