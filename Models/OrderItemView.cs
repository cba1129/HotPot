using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class OrderItemView
{
    public int OrderItemId { get; set; }

    public int OrderItemOrderId { get; set; }

    public int OrderItemMenuId { get; set; }

    public int OrderItemQuantity { get; set; }

    public decimal OrderItemUnitPrice { get; set; }

    public virtual Menu OrderItemMenu { get; set; } = null!;

    public virtual OrderView OrderItemOrder { get; set; } = null!;
}
