using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Hotel
{
    public int HotelId { get; set; }
    [Required]
    public string HotelName { get; set; } = null!;
    [Required]
    public string HotelImage { get; set; } = null!;
    [Required]
    public string HotelDescription { get; set; } = null!;
    [Required]
    public string HotelLocation { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
