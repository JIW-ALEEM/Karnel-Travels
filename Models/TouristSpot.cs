using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class TouristSpot
{
    public int SpotId { get; set; }
    [Required]
    public string SpotName { get; set; } = null!;

	public string? SpotImage { get; set; }
    [Required]
    public long SpotPrice { get; set; }
    [Required]
    public string SpotDescription { get; set; } = null!;
    [Required]
    public string SpotLocation { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
