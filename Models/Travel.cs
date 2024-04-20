using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Travel
{
    public int TravelId { get; set; }
    [Required]
    public string TravelMode { get; set; } = null!;

    public string? TravelImage { get; set; }
    [Required]
    public long TravelPrice { get; set; }
    [Required]
    public string TravelDescription { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
