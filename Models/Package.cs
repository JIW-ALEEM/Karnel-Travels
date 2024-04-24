using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Package
{
    public int PackageId { get; set; }
    [Required]
    public string PackageName { get; set; } = null!;

	public string? PackageImage { get; set; } 
    [Required]
    public string PackagePerson { get; set; } = null!;
    [Required]
    public string PackageTour { get; set; } = null!;
    [Required]
    public long PackagePrice { get; set; }
    [Required]
    public string PackageDescription { get; set; } = null!;

    public int? PackageTouristSpotId { get; set; }

    public int? PackageTravelId { get; set; }

    public int? PackageHotelId { get; set; }

    public int? PackageRestaurantId { get; set; }

    public int? PackageResortId { get; set; }

    public virtual Hotel? PackageHotel { get; set; }

    public virtual Resort? PackageResort { get; set; }

    public virtual Restaurant? PackageRestaurant { get; set; }

    public virtual TouristSpot? PackageTouristSpot { get; set; }

    public virtual Travel? PackageTravel { get; set; }
}
