using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models;

public partial class HotPotContext : DbContext
{
    public HotPotContext()
    {
    }

    public HotPotContext(DbContextOptions<HotPotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carousel> Carousels { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<RestaurantInfo> RestaurantInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:HotPotConnstring");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carousel>(entity =>
        {
            entity.HasKey(e => e.CarouselCarouselId).HasName("PK__Carousel__EC8FB1C43F008C23");

            entity.ToTable("Carousel");

            entity.Property(e => e.CarouselCarouselId).HasColumnName("Carousel_CarouselId");
            entity.Property(e => e.CarouselCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Carousel_CreatedAt");
            entity.Property(e => e.CarouselDescription).HasColumnName("Carousel_Description");
            entity.Property(e => e.CarouselDisplayOrder).HasColumnName("Carousel_DisplayOrder");
            entity.Property(e => e.CarouselEndTime).HasColumnName("Carousel_EndTime");
            entity.Property(e => e.CarouselImageUrl)
                .HasMaxLength(255)
                .HasColumnName("Carousel_ImageUrl");
            entity.Property(e => e.CarouselIsActive)
                .HasDefaultValue(true)
                .HasColumnName("Carousel_IsActive");
            entity.Property(e => e.CarouselLinkUrl)
                .HasMaxLength(255)
                .HasColumnName("Carousel_LinkUrl");
            entity.Property(e => e.CarouselStartTime).HasColumnName("Carousel_StartTime");
            entity.Property(e => e.CarouselTitle)
                .HasMaxLength(100)
                .HasColumnName("Carousel_Title");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerCustomerId).HasName("PK__Customer__DF3BDDE9B3504256");

            entity.HasIndex(e => e.CustomerPhone, "UQ__Customer__564718C4E17A96B2").IsUnique();

           // entity.HasIndex(e => e.CustomerAccount, "UQ__Customer__5A6A5323489B15E4").IsUnique();

            entity.HasIndex(e => e.CustomerEmail, "UQ__Customer__8A8E9747957F75C7").IsUnique();

            entity.Property(e => e.CustomerCustomerId).HasColumnName("Customer_CustomerId");
            entity.Property(e => e.CustomerAccount)
                .HasMaxLength(50)
                .HasColumnName("Customer_Account");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .HasColumnName("Customer_Address");
            //entity.Property(e => e.CustomerBirthDate).HasColumnName("Customer_BirthDate");
            entity.Property(e => e.CustomerCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Customer_CreatedAt");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .HasColumnName("Customer_Email");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("Customer_Name");
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(256)
                .HasColumnName("Customer_Password");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(20)
                .HasColumnName("Customer_Phone");
            //entity.Property(e => e.CustomerPoints)
                //.HasDefaultValue(0.00m)
                //.HasColumnType("decimal(10, 2)")
                //.HasColumnName("Customer_Points");
        });

        modelBuilder.Entity<CustomerFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackFeedbackId).HasName("PK__Customer__F42951E598345AB1");

            entity.ToTable("CustomerFeedback");

            entity.Property(e => e.FeedbackFeedbackId).HasColumnName("Feedback_FeedbackId");
            entity.Property(e => e.FeedbackContent).HasColumnName("Feedback_Content");
            entity.Property(e => e.FeedbackDiningLocation)
                .HasMaxLength(100)
                .HasColumnName("Feedback_DiningLocation");
            entity.Property(e => e.FeedbackEmail)
                .HasMaxLength(255)
                .HasColumnName("Feedback_Email");
            entity.Property(e => e.FeedbackGender)
                .HasMaxLength(50)
                .HasColumnName("Feedback_Gender");
            entity.Property(e => e.FeedbackName)
                .HasMaxLength(100)
                .HasColumnName("Feedback_Name");
            entity.Property(e => e.FeedbackPhone)
                .HasMaxLength(20)
                .HasColumnName("Feedback_Phone");
            entity.Property(e => e.FeedbackTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Feedback_Time");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuMenuId).HasName("PK__Menu__8A6E67B80F697CD0");

            entity.ToTable("Menu");

            entity.Property(e => e.MenuMenuId).HasColumnName("Menu_MenuId");
            entity.Property(e => e.MenuCategory)
                .HasMaxLength(50)
                .HasColumnName("Menu_Category");
            entity.Property(e => e.MenuDescription).HasColumnName("Menu_Description");
            entity.Property(e => e.MenuIsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("Menu_IsAvailable");
            entity.Property(e => e.MenuItemName)
                .HasMaxLength(100)
                .HasColumnName("Menu_ItemName");
            entity.Property(e => e.MenuPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Menu_Price");
            entity.Property(e => e.MenuRestaurantId).HasColumnName("Menu_RestaurantId");

            entity.HasOne(d => d.MenuRestaurant).WithMany(p => p.Menus)
                .HasForeignKey(d => d.MenuRestaurantId)
                .HasConstraintName("FK__Menu__Menu_Resta__45F365D3");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderOrderId).HasName("PK__Orders__DFD746E7F08A98E5");

            entity.Property(e => e.OrderOrderId).HasColumnName("Order_OrderId");
            entity.Property(e => e.OrderCustomerId).HasColumnName("Order_CustomerId");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderRestaurantId).HasColumnName("Order_RestaurantId");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("Order_Status");
            entity.Property(e => e.OrderTotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Order_TotalAmount");
            entity.Property(e => e.OrderType)
                .HasMaxLength(50)
                .HasColumnName("Order_Type");

            entity.HasOne(d => d.OrderCustomer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderCustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Orders__Order_Cu__4CA06362");

            entity.HasOne(d => d.OrderRestaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderRestaurantId)
                .HasConstraintName("FK__Orders__Order_Re__4D94879B");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemItemId).HasName("PK__OrderIte__A3428DFA2D16F919");

            entity.Property(e => e.OrderItemItemId).HasColumnName("OrderItem_ItemId");
            entity.Property(e => e.OrderItemMenuId).HasColumnName("OrderItem_MenuId");
            entity.Property(e => e.OrderItemOrderId).HasColumnName("OrderItem_OrderId");
            entity.Property(e => e.OrderItemQuantity).HasColumnName("OrderItem_Quantity");
            entity.Property(e => e.OrderItemUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("OrderItem_UnitPrice");

            entity.HasOne(d => d.OrderItemMenu).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__534D60F1");

            entity.HasOne(d => d.OrderItemOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemOrderId)
                .HasConstraintName("FK__OrderItem__Order__52593CB8");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentPaymentId).HasName("PK__Payments__F8DFE7150F380159");

            entity.Property(e => e.PaymentPaymentId).HasColumnName("Payment_PaymentId");
            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Payment_Amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Payment_Date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("Payment_Method");
            entity.Property(e => e.PaymentOrderId).HasColumnName("Payment_OrderId");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("Payment_Status");

            entity.HasOne(d => d.PaymentOrder).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentOrderId)
                .HasConstraintName("FK__Payments__Paymen__5FB337D6");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationReservationId).HasName("PK__Reservat__F2065D8215596CC7");

            entity.Property(e => e.ReservationReservationId).HasColumnName("Reservation_ReservationId");
            entity.Property(e => e.ReservationCustomerId).HasColumnName("Reservation_CustomerId");
            entity.Property(e => e.ReservationRestaurantId).HasColumnName("Reservation_RestaurantId");
            entity.Property(e => e.ReservationStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("Reservation_Status");
            entity.Property(e => e.ReservationTime).HasColumnName("Reservation_Time");

            entity.HasOne(d => d.ReservationCustomer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReservationCustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Reservati__Reser__5812160E");

            entity.HasOne(d => d.ReservationRestaurant).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ReservationRestaurantId)
                .HasConstraintName("FK__Reservati__Reser__59063A47");
        });

        modelBuilder.Entity<RestaurantInfo>(entity =>
        {
            entity.HasKey(e => e.RestaurantRestaurantId).HasName("PK__Restaura__AD2E937A5B4CA653");

            entity.ToTable("RestaurantInfo");

            entity.HasIndex(e => e.RestaurantPhone, "UQ__Restaura__A5FDD72D95379FAB").IsUnique();

            entity.HasIndex(e => e.RestaurantEmail, "UQ__Restaura__ECB000DCE33DE13C").IsUnique();

            entity.Property(e => e.RestaurantRestaurantId).HasColumnName("Restaurant_RestaurantId");
            entity.Property(e => e.RestaurantAddress)
                .HasMaxLength(255)
                .HasColumnName("Restaurant_Address");
            entity.Property(e => e.RestaurantCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Restaurant_CreatedAt");
            entity.Property(e => e.RestaurantDescription).HasColumnName("Restaurant_Description");
            entity.Property(e => e.RestaurantEmail)
                .HasMaxLength(100)
                .HasColumnName("Restaurant_Email");
            entity.Property(e => e.RestaurantImageUrl)
                .HasMaxLength(255)
                .HasColumnName("Restaurant_ImageUrl");
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(100)
                .HasColumnName("Restaurant_Name");
            entity.Property(e => e.RestaurantOpeningHours)
                .HasMaxLength(255)
                .HasColumnName("Restaurant_OpeningHours");
            entity.Property(e => e.RestaurantPhone)
                .HasMaxLength(20)
                .HasColumnName("Restaurant_Phone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
