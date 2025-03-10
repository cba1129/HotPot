using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class ReservationView
{
    public int ReservationId { get; set; }

    public int CustomerId { get; set; }

    public int RestaurantId { get; set; }

    public string ReservationName { get; set; } = null!;

    public string ReservationPhone { get; set; } = null!;

    public int ReservationPeople { get; set; }

    public DateTime ReservationDate { get; set; }

    public DateTime ReservationCreatedDate { get; set; }

    public virtual CustomerView Customer { get; set; } = null!;

    public virtual RestaurantInfoView Restaurant { get; set; } = null!;
}
