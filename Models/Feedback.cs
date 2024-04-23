using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;

namespace Karnel_Travels.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }
    [Required]
    public string FeedbackUserName { get; set; } = null!;
    [Required]
    public string FeedbackUserEmail { get; set; } = null!;
    [Required]
    public string FeedbackMassage { get; set; } = null!;

    public int? SelectedId { get; set; }

    public int? FeedbackUserId { get; set; }

    public virtual User? FeedbackUser { get; set; }
}
