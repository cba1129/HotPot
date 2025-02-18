using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Carousel
{
    public int CarouselCarouselId { get; set; }

    public string CarouselImageUrl { get; set; } = null!;

    public string? CarouselTitle { get; set; }

    public string? CarouselDescription { get; set; }

    public string? CarouselLinkUrl { get; set; }

    public int CarouselDisplayOrder { get; set; }

    public bool? CarouselIsActive { get; set; }

    public DateTime? CarouselStartTime { get; set; }

    public DateTime? CarouselEndTime { get; set; }

    public DateTime? CarouselCreatedAt { get; set; }
}
