using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

    public int? FeedbackUserId { get; set; }

    public int? FeedbackTouristSpotId { get; set; }

    public int? FeedbackTravelId { get; set; }

    public int? FeedbackHotelId { get; set; }

    public int? FeedbackRestaurantId { get; set; }

    public int? FeedbackResortId { get; set; }

    public int? FeedbackPackageId { get; set; }

    public virtual Hotel? FeedbackHotel { get; set; }

    public virtual Package? FeedbackPackage { get; set; }

    public virtual Resort? FeedbackResort { get; set; }

    public virtual Restaurant? FeedbackRestaurant { get; set; }

    public virtual TouristSpot? FeedbackTouristSpot { get; set; }

    public virtual Travel? FeedbackTravel { get; set; }

    public virtual User? FeedbackUser { get; set; }
}
