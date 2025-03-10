using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Carousel
{
    public int CarouselId { get; set; }

    public string CarouselImageUrl { get; set; } = null!;

    public string? CarouselTitle { get; set; }

    public int CarouselDisplayOrder { get; set; }

    public bool? CarouselIsActive { get; set; }

    public DateTime? CarouselStartTime { get; set; }

    public DateTime? CarouselEndTime { get; set; }

    public DateTime? CarouselCreatedAt { get; set; }
}
