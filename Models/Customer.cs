using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models;

public partial class Customer
{
    
    public int CustomerCustomerId { get; set; }


    [Required(ErrorMessage = "請輸入姓名")]
    [Display(Name = "姓名")]
    public string CustomerName { get; set; } = null!;


    [Required(ErrorMessage = "請輸入有效的 電話")]
    [Display(Name = "電話")]
    public string CustomerPhone { get; set; } = null!;


    [Required(ErrorMessage = "請輸入有效的 Email")]
    [Display(Name = "E-mail")]
    public string CustomerEmail { get; set; } = null!;

    // public string CustomerGender { get; set; } = null!;
    [Required(ErrorMessage = "請輸入帳號")]
    [Display(Name = "帳號")]
    public string CustomerAccount { get; set; } = null!;

    
    [Required(ErrorMessage = "請輸入密碼 至少8位")]    
    [DataType(DataType.Password)]
    [Display(Name = "密碼")]
    public string CustomerPassword { get; set; } = null!;

    //[Required]
    //[DataType(DataType.Password)]
    //[Compare("CustomerPassword", ErrorMessage = "密碼與確認密碼不相符")]
    //public string? ConfirmPassword { get; set; }

    //[Display(Name = "生日")]
    //public DateOnly? CustomerBirthDate { get; set; }


    //[Display(Name = "點數")]
    //public decimal? CustomerPoints { get; set; }


    [Required(ErrorMessage = "請輸入有效的地址")]
    [Display(Name = "地址")]
    public string? CustomerAddress { get; set; }

  
    public DateTime? CustomerCreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

    


