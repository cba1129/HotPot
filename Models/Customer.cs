using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public partial class Customer
{
    
    public int CustomerCustomerId { get; set; }

    [Display(Name = "姓名")]
    public string CustomerName { get; set; } = null!;

    [Display(Name = "電話")]
    public string CustomerPhone { get; set; } = null!;

    [Display(Name = "E-mail")]
    public string CustomerEmail { get; set; } = null!;

    [Display(Name = "密碼")]
    public byte[] CustomerPassword { get; set; } = null!;

    [Display(Name = "生日")]
    public DateOnly? CustomerBirthDate { get; set; }

    [Display(Name = "帳號")]
    public string CustomerAccount { get; set; } = null!;

    [Display(Name = "點數")]
    public decimal? CustomerPoints { get; set; }

    [Display(Name = "地址")]
    public string? CustomerAddress { get; set; }

  
    public DateTime? CustomerCreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
