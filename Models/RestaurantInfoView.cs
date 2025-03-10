using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class RestaurantInfoView
{
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string RestaurantAddress { get; set; } = null!;

    public string RestaurantPhone { get; set; } = null!;

    public string RestaurantImageUrl { get; set; } = null!;

    public string RestaurantOpeningHours { get; set; } = null!;

    public string? RestaurantLastOrderTime { get; set; }

    public string? RestaurantMapEmbedUrl { get; set; }

    public string? RestaurantInfoPosition { get; set; }

    public int RestaurantCapacity { get; set; }

    public int RestaurantReservationCount { get; set; }

    public DateTime RestaurantCreatedAt { get; set; }

    public virtual ICollection<CustomerFeedbackView> CustomerFeedbacks { get; set; } = new List<CustomerFeedbackView>();

    public virtual ICollection<OrderView> Orders { get; set; } = new List<OrderView>();

    public virtual ICollection<ReservationView> Reservations { get; set; } = new List<ReservationView>();
}
