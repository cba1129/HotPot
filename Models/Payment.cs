using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Payment
{
    public int PaymentPaymentId { get; set; }

    public int PaymentOrderId { get; set; }

    public decimal PaymentAmount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Order PaymentOrder { get; set; } = null!;
}
