using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Menu
{
    public int MenuMenuId { get; set; }

    public string MenuItemName { get; set; } = null!;

    public string? MenuDescription { get; set; }

    public decimal MenuPrice { get; set; }

    public string MenuCategory { get; set; } = null!;

    public bool? MenuIsAvailable { get; set; }

    public int MenuRestaurantId { get; set; }

    public virtual RestaurantInfo MenuRestaurant { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
