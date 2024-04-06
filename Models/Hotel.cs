using System;
using System.Collections.Generic;

namespace Karnel_Travels.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string HotelImage { get; set; } = null!;

    public string HotelDescription { get; set; } = null!;

    public string HotelLocation { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
