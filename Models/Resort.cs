using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class Resort
{
    public int ResortId { get; set; }
    [Required]
    public string ResortName { get; set; } = null!;

	public string? ResortImage { get; set; } 
    [Required]
    public long ResortPrice { get; set; }
    [Required]
    public string ResortDescription { get; set; } = null!;
    [Required]
    public string ResortLocation { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; } = new List<Package>();
}
