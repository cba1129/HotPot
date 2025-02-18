using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Order
{
    public int OrderOrderId { get; set; }

    public int? OrderCustomerId { get; set; }

    public int OrderRestaurantId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal OrderTotalAmount { get; set; }

    public string? OrderStatus { get; set; }

    public string OrderType { get; set; } = null!;

    public virtual Customer? OrderCustomer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual RestaurantInfo OrderRestaurant { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
