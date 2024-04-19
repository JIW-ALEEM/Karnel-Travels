using Karnel_Travels.Data;
using Karnel_Travels.Models;
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
        public IActionResult AdminIndex()
        {
            return View();
        }



        // Tourist Spot Form
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
        public IActionResult UpdateSpot(int? id)
        {
            var data = db.TouristSpots.FirstOrDefault(x => x.SpotId == id);
            return View(data);
        }

        // Update Tourist Spot action method
        public IActionResult UpdateSpot2(TouristSpot UpdateSpot2, IFormFile file)
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
        [HttpGet]
        public IActionResult Travel()
        {
            return View();
        }
        // Travel Insertion
        [HttpPost]
        public IActionResult Travel(Travel catg)
        {
            if (ModelState.IsValid)
            {
                db.Add(catg);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchTravel));
            }
            return View();
        }

        // Fetch Travel
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
        [HttpGet]
        public IActionResult UpdateTravel(int? id)
        {
            var data = db.Travels.FirstOrDefault(x => x.TravelId == id);
            return View(data);
        }

        // Update Travel action method
        [HttpPost]
        public IActionResult UpdateTravel(Travel UpdateTravel) {
            if (ModelState.IsValid)
            {
                db.Update(UpdateTravel);
            db.SaveChanges();
            TempData["UpdateMessage"] = "Record Updated Successfully"; 
                return RedirectToAction(nameof(FetchTravel));
            }
            return View();
        }

        // Hotel Form
        public IActionResult Hotel()
        {
            return View();
        }

        // Image Insertion in Hotel
        public IActionResult AddHotel(Hotel Hotel1, IFormFile HotelImage)
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
                db.Add(Hotel1);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchHotel));
            }
            return View();
        }

        // Fetch Hotel
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
        public IActionResult UpdateHotel(int? id)
        {
            var data = db.Hotels.FirstOrDefault(x => x.HotelId == id);
            return View(data);
        }

        // Update Hotel action method
        public IActionResult UpdateHotel2(Hotel UpdateHotel2, IFormFile file)
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
                UpdateHotel2.HotelImage = dbPath;
                db.Update(UpdateHotel2);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchHotel));
            }
            else
            {
                var existingHotel = db.Hotels.FirstOrDefault(p => p.HotelId == UpdateHotel2.HotelId);
                if (existingHotel != null)
                {
                    UpdateHotel2.HotelImage = existingHotel.HotelImage;

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
            db.Update(UpdateHotel2);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchHotel));
        }







        // Restaurant Form
        public IActionResult Restaurant()
        {
            return View();
        }

        // Image Insertion in Restaurant
        public IActionResult AddRestaurant(Restaurant Restaurant1, IFormFile RestaurantImage)
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
                db.Add(Restaurant1);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchRestaurant));
            }
            return View();
        }

        // Fetch Restaurant
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
        public IActionResult UpdateRestaurant(int? id)
        {
            var data = db.Restaurants.FirstOrDefault(x => x.RestaurantId == id);
            return View(data);
        }

        // Update Restaurant action method
        public IActionResult UpdateRestaurant2(Restaurant UpdateRestaurant2, IFormFile file)
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
                UpdateRestaurant2.RestaurantImage = dbPath;
                db.Update(UpdateRestaurant2);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchRestaurant));
            }
            else
            {
                var existingRestaurant = db.Restaurants.FirstOrDefault(p => p.RestaurantId == UpdateRestaurant2.RestaurantId);
                if (existingRestaurant != null)
                {
                    UpdateRestaurant2.RestaurantImage = existingRestaurant.RestaurantImage;

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
            db.Update(UpdateRestaurant2);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchRestaurant));
        }






        // Resort Form
        public IActionResult Resort()
        {
            return View();
        }

        // Image Insertion in Resort
        public IActionResult AddResort(Resort Resort1, IFormFile ResortImage)
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
                db.Add(Resort1);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchResort));
            }
            return View();
        }

        // Fetch Resort
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
        public IActionResult UpdateResort(int? id)
        {
            var data = db.Resorts.FirstOrDefault(x => x.ResortId == id);
            return View(data);
        }

        // Update Resort action method
        public IActionResult UpdateResort2(Resort UpdateResort2, IFormFile file)
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
                UpdateResort2.ResortImage = dbPath;
                db.Update(UpdateResort2);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchResort));
            }
            else
            {
                var existingRestaurant = db.Resorts.FirstOrDefault(p => p.ResortId == UpdateResort2.ResortId);
                if (existingRestaurant != null)
                {
                    UpdateResort2.ResortImage = existingRestaurant.ResortImage;

                    // Detach existing tracked entity
                    db.Entry(existingRestaurant).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Product not found";
                    return RedirectToAction(nameof(FetchResort));
                }
            }

            // Update entity state
            db.Update(UpdateResort2);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchResort));
        }







        public IActionResult Package()
        {
            ViewBag.Travel = new SelectList(db.Travels, "TravelId", "TravelMode");
            ViewBag.TouristSpot = new SelectList(db.TouristSpots, "SpotId", "SpotName");
            ViewBag.Restaurant = new SelectList(db.Restaurants, "RestaurantId", "RestaurantName");
            ViewBag.Resort = new SelectList(db.Resorts, "ResortId", "ResortName");
            ViewBag.Hotel = new SelectList(db.Hotels, "HotelId", "HotelName");
            return View();
        }
        public IActionResult AddPackage(Package pack, IFormFile PackageImage)
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
                pack.PackageImage = dbpath;
                db.Add(pack);
                db.SaveChanges();
                TempData["Message"] = "Record Inserted Successfully";
                return RedirectToAction(nameof(FetchPackage));
            }
            return View();
        }
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


        public IActionResult DeletePackage(int? id)
        {
            var data = db.Packages.FirstOrDefault(x => x.PackageId == id);
            db.Remove(data);
            db.SaveChanges();
            TempData["DelMessage"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(FetchPackage));
        }

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

        public IActionResult UpdatePackage2(Package UpdatePackage2, IFormFile file)
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
                UpdatePackage2.PackageImage = dbPath;
                db.Update(UpdatePackage2);
                db.SaveChanges();
                TempData["UpdateMessage"] = "Record Updated Successfully";
                return RedirectToAction(nameof(FetchPackage));
            }
            else
            {
                var existingPackage = db.Packages.FirstOrDefault(p => p.PackageId == UpdatePackage2.PackageId);
                if (existingPackage != null)
                {
                    UpdatePackage2.PackageImage = existingPackage.PackageImage;

                    // Detach existing tracked entity
                    db.Entry(existingPackage).State = EntityState.Detached;
                }
                else
                {
                    TempData["ErrorMessage"] = "Package not found";
                    return RedirectToAction(nameof(FetchPackage));
                }
            }

            // Update entity state
            db.Update(UpdatePackage2);
            db.SaveChanges();

            TempData["UpdateMessage"] = file != null ? "Record Updated Successfully" : "Record Updated Successfully with Previous Image";
            return RedirectToAction(nameof(FetchPackage));
        }
    }
}
