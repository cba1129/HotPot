using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class CustomerFeedback
{
    public int FeedbackFeedbackId { get; set; }

    public string FeedbackName { get; set; } = null!;

    public string FeedbackGender { get; set; } = null!;

    public string FeedbackDiningLocation { get; set; } = null!;

    public string FeedbackPhone { get; set; } = null!;

    public string FeedbackEmail { get; set; } = null!;

    public string FeedbackContent { get; set; } = null!;

    public DateTime? FeedbackTime { get; set; }
}
