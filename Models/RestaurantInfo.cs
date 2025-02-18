using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class RestaurantInfo
{
    public int RestaurantRestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string RestaurantAddress { get; set; } = null!;

    public string RestaurantPhone { get; set; } = null!;

    public string? RestaurantEmail { get; set; }

    public string? RestaurantImageUrl { get; set; }

    public string? RestaurantDescription { get; set; }

    public string? RestaurantOpeningHours { get; set; }

    public DateTime? RestaurantCreatedAt { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
