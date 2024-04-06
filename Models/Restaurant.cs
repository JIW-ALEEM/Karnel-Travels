using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Restaurant
{
    public int RestaurantId { get; set; }
    [Required]
    public string RestaurantName { get; set; } = null!;
    [Required]
    public string RestaurantImage { get; set; } = null!;
    [Required]
    public string RestaurantMenu { get; set; } = null!;
    [Required]
    public string RestaurantDescription { get; set; } = null!;
    [Required]
    public string RestaurantLocation { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
