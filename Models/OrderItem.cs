using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class OrderItem
{
    public int OrderItemItemId { get; set; }

    public int OrderItemOrderId { get; set; }

    public int OrderItemMenuId { get; set; }

    public int OrderItemQuantity { get; set; }

    public decimal OrderItemUnitPrice { get; set; }

    public virtual Menu OrderItemMenu { get; set; } = null!;

    public virtual Order OrderItemOrder { get; set; } = null!;
}
