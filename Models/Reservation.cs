using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Reservation
{
    public int ReservationReservationId { get; set; }

    public int? ReservationCustomerId { get; set; }

    public int ReservationRestaurantId { get; set; }

    public DateTime ReservationTime { get; set; }

    public string? ReservationStatus { get; set; }

    public virtual Customer? ReservationCustomer { get; set; }

    public virtual RestaurantInfo ReservationRestaurant { get; set; } = null!;
}
