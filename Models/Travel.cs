using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Travel
{
    public int TravelId { get; set; }
    [Required]
    public string TravelMode { get; set; } = null!;
    [Required]
    public string TravelDescription { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
