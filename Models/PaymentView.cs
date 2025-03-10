using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class PaymentView
{
    public int PaymentId { get; set; }

    public int PaymentOrderId { get; set; }

    public decimal PaymentAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual OrderView PaymentOrder { get; set; } = null!;
}
