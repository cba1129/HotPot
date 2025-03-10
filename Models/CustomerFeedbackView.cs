using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

[Keyless]
public class CustomerFeedbackView
{
    public int FeedbackId { get; set; }

    [Required(ErrorMessage = "請輸入姓名")]
    [Display(Name = "姓名")]
    public string? FeedbackName { get; set; }

    [Required(ErrorMessage = "請選擇性別")]
    [Display(Name = "性別")]
    public string? FeedbackGender { get; set; }

    [Required(ErrorMessage = "請選擇用餐時段")]
    [Display(Name = "用餐時段")]
    public DateTime? FeedbackDateTime { get; set; }

    [Required(ErrorMessage = "請選擇分店")]
    [Display(Name = "用餐門市")]
    public int FeedbackDiningLocationId { get; set; }

    [Display(Name = "用餐門市")]
    public string? FeedbackDiningLocation { get; set; }

    
    [Required(ErrorMessage = "請選擇用餐品項")]
    [NotMapped]
    [Display(Name = "用餐品項")]
    public int? FeedbackMenuId { get; set; }

    [Display(Name = "用餐品項")]
    [NotMapped]
    public string? FeedbackMenuName { get; set; }

    [Required(ErrorMessage = "請輸入手機號碼")]
    [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "手機號碼格式錯誤")]
    [Display(Name = "電話")]
    public string? FeedbackPhone { get; set; }

    [Required(ErrorMessage = "請輸入電子郵件")]
    [EmailAddress(ErrorMessage = "請輸入有效的電子郵件地址")]
    [Display(Name = "Email")]
    public string? FeedbackEmail { get; set; }

    [Required(ErrorMessage = "請輸入您的意見")]
    [Display(Name = "問題建議")]
    public string? FeedbackContent { get; set; }

    [Display(Name = "填寫時間")]
    public DateTime? FeedbackTime { get; set; }
    public virtual RestaurantInfoView? FeedbackDiningLocationNavigation { get; set; }
}
