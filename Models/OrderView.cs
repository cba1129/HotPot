using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

public partial class OrderView
{
    public int OrderId { get; set; }

    public int OrderCustomerId { get; set; }

    public int OrderRestaurantId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal OrderTotalAmount { get; set; }

    public string OrderType { get; set; } = null!;

    [NotMapped]
    public string OrderMenuName { get; set; } = null!;

    [NotMapped]
    public string OrderName { get; set; } = null!;

    [NotMapped]
    public string OrderPhone { get; set; } = null!;

    [NotMapped]
    public string OrderAddress { get; set; } = null!;

    [NotMapped]
    public int RegionAddress { get; set; }

    [NotMapped]
    public string StreetAddress { get; set; } = null!;

    [NotMapped]
    public string? OrderMenuImage { get; set; } = null!;

    [NotMapped]
    public string? OrderMenuDescription { get; set; } = null!;

    [NotMapped]
    public int OrderMenuId { get; set; }

    [NotMapped]
    public decimal OrderMenuPrice { get; set; }

    public virtual CustomerView OrderCustomer { get; set; } = null!;

    public virtual ICollection<OrderItemView> OrderItems { get; set; } = new List<OrderItemView>();

    public virtual RestaurantInfoView OrderRestaurant { get; set; } = null!;

    public virtual ICollection<PaymentView> Payments { get; set; } = new List<PaymentView>();
}
