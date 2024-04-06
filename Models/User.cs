using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Karnel_Travels.Models;

public partial class User
{
    public int UserId { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string UserEmail { get; set; } = null!;
    [Required]
    public string UserPassword { get; set; } = null!;

    public int? UserRoleId { get; set; }

    public virtual Role? UserRole { get; set; }
}
