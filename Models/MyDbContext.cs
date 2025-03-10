using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carousel> Carousels { get; set; }

    public virtual DbSet<CustomerView> Customers { get; set; }

    public virtual DbSet<CustomerFeedbackView> CustomerFeedbacks { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<OrderView> Orders { get; set; }

    public virtual DbSet<OrderItemView> OrderItems { get; set; }

    public virtual DbSet<PaymentView> Payments { get; set; }

    public virtual DbSet<ReservationView> Reservations { get; set; }

    public virtual DbSet<RestaurantInfoView> RestaurantInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=10.0.101.160,1433;Initial Catalog=RestaurantDB;User Id=TeamHotPots;Password=MSIT62;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carousel>(entity =>
        {
            entity.HasKey(e => e.CarouselId).HasName("PK__Carousel__7EE7615BDD5E5554");

            entity.ToTable("Carousel");

            entity.Property(e => e.CarouselId).HasColumnName("Carousel_Id");
            entity.Property(e => e.CarouselCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Carousel_CreatedAt");
            entity.Property(e => e.CarouselDisplayOrder).HasColumnName("Carousel_DisplayOrder");
            entity.Property(e => e.CarouselEndTime).HasColumnName("Carousel_EndTime");
            entity.Property(e => e.CarouselImageUrl)
                .HasMaxLength(255)
                .HasColumnName("Carousel_ImageUrl");
            entity.Property(e => e.CarouselIsActive)
                .HasDefaultValue(true)
                .HasColumnName("Carousel_IsActive");
            entity.Property(e => e.CarouselStartTime).HasColumnName("Carousel_StartTime");
            entity.Property(e => e.CarouselTitle)
                .HasMaxLength(100)
                .HasColumnName("Carousel_Title");
        });

        modelBuilder.Entity<CustomerView>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8CB2869968B1C603");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.CustomerAccount)
                .HasMaxLength(50)
                .HasColumnName("Customer_Account");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(255)
                .HasColumnName("Customer_Address");
            entity.Property(e => e.CustomerCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Customer_CreatedAt");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .HasColumnName("Customer_Email");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(20)
                .HasColumnName("Customer_Name");
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(256)
                .HasColumnName("Customer_Password");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(20)
                .HasColumnName("Customer_Phone");
        });

        modelBuilder.Entity<CustomerFeedbackView>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Customer__CD3993185A262292");

            entity.ToTable("CustomerFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("Feedback_Id");
            entity.Property(e => e.FeedbackContent).HasColumnName("Feedback_Content");
            entity.Property(e => e.FeedbackDateTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Feedback_DateTime");
            entity.Property(e => e.FeedbackDiningLocation)
                .HasMaxLength(20)
                .HasColumnName("Feedback_DiningLocation");
            entity.Property(e => e.FeedbackDiningLocationId).HasColumnName("Feedback_DiningLocationId");
            entity.Property(e => e.FeedbackEmail)
                .HasMaxLength(255)
                .HasColumnName("Feedback_Email");
            entity.Property(e => e.FeedbackGender)
                .HasMaxLength(10)
                .HasColumnName("Feedback_Gender");
            entity.Property(e => e.FeedbackName)
                .HasMaxLength(20)
                .HasColumnName("Feedback_Name");
            entity.Property(e => e.FeedbackPhone)
                .HasMaxLength(20)
                .HasColumnName("Feedback_Phone");
            entity.Property(e => e.FeedbackTime)
                .HasMaxLength(20)
                .HasColumnName("Feedback_Time");
            entity.HasOne(d => d.FeedbackDiningLocationNavigation).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.FeedbackDiningLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerF__Feedb__5CD6CB2B");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__69E72338B04A1585");

            entity.ToTable("Menu");

            entity.Property(e => e.MenuId).HasColumnName("Menu_Id");
            entity.Property(e => e.MenuCategory)
                .HasMaxLength(8)
                .HasColumnName("Menu_Category");
            entity.Property(e => e.MenuDescription)
                .HasMaxLength(100)
                .HasColumnName("Menu_Description");
            entity.Property(e => e.MenuImageUrl)
                .HasMaxLength(100)
                .HasColumnName("Menu_ImageUrl");
            entity.Property(e => e.MenuIsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("Menu_IsAvailable");
            entity.Property(e => e.MenuName)
                .HasMaxLength(20)
                .HasColumnName("Menu_Name");
            entity.Property(e => e.MenuPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Menu_Price");
        });

        modelBuilder.Entity<OrderView>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__F1E4607B47F27BCA");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.OrderCustomerId).HasColumnName("Order_CustomerId");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Order_Date");
            entity.Property(e => e.OrderRestaurantId).HasColumnName("Order_RestaurantId");
            entity.Property(e => e.OrderTotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Order_TotalAmount");
            entity.Property(e => e.OrderType)
                .HasMaxLength(50)
                .HasDefaultValue("Pending")
                .HasColumnName("Order_Type");

            entity.HasOne(d => d.OrderCustomer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderCustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Order_Cu__440B1D61");

            entity.HasOne(d => d.OrderRestaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderRestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Order_Re__44FF419A");
        });

        modelBuilder.Entity<OrderItemView>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__2F3022028ABA6856");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_Id");
            entity.Property(e => e.OrderItemMenuId).HasColumnName("OrderItem_MenuId");
            entity.Property(e => e.OrderItemOrderId).HasColumnName("OrderItem_OrderId");
            entity.Property(e => e.OrderItemQuantity).HasColumnName("OrderItem_Quantity");
            entity.Property(e => e.OrderItemUnitPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("OrderItem_UnitPrice");

            entity.HasOne(d => d.OrderItemMenu).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__49C3F6B7");

            entity.HasOne(d => d.OrderItemOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderItemOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__48CFD27E");
        });

        modelBuilder.Entity<PaymentView>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__DA6C7FC1BEF711E6");

            entity.Property(e => e.PaymentId).HasColumnName("Payment_Id");
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__Paymen__5441852A");
        });

        modelBuilder.Entity<ReservationView>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__17302AAD7913F6D4");

            entity.Property(e => e.ReservationId).HasColumnName("Reservation_Id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.ReservationCreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Reservation_CreatedDate");
            entity.Property(e => e.ReservationDate)
                .HasColumnType("datetime")
                .HasColumnName("Reservation_Date");
            entity.Property(e => e.ReservationName)
                .HasMaxLength(50)
                .HasColumnName("Reservation_Name");
            entity.Property(e => e.ReservationPeople).HasColumnName("Reservation_People");
            entity.Property(e => e.ReservationPhone)
                .HasMaxLength(20)
                .HasColumnName("Reservation_Phone");
            entity.Property(e => e.RestaurantId).HasColumnName("Restaurant_Id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Custo__4D94879B");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__Reservati__Resta__4E88ABD4");
        });

        modelBuilder.Entity<RestaurantInfoView>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__B422BC6BCF489B67");

            entity.ToTable("RestaurantInfo");

            entity.Property(e => e.RestaurantId).HasColumnName("Restaurant_Id");
            entity.Property(e => e.RestaurantAddress)
                .HasMaxLength(255)
                .HasColumnName("Restaurant_Address");
            entity.Property(e => e.RestaurantCapacity).HasColumnName("Restaurant_Capacity");
            entity.Property(e => e.RestaurantCreatedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Restaurant_CreatedAt");
            entity.Property(e => e.RestaurantImageUrl)
                .HasMaxLength(255)
                .HasColumnName("Restaurant_ImageUrl");
            entity.Property(e => e.RestaurantInfoPosition)
                .HasMaxLength(10)
                .HasColumnName("Restaurant_InfoPosition");
            entity.Property(e => e.RestaurantLastOrderTime)
                .HasMaxLength(50)
                .HasColumnName("Restaurant_LastOrderTime");
            entity.Property(e => e.RestaurantMapEmbedUrl)
                .HasMaxLength(500)
                .HasColumnName("Restaurant_MapEmbedUrl");
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(20)
                .HasColumnName("Restaurant_Name");
            entity.Property(e => e.RestaurantOpeningHours)
                .HasMaxLength(20)
                .HasColumnName("Restaurant_OpeningHours");
            entity.Property(e => e.RestaurantPhone)
                .HasMaxLength(20)
                .HasColumnName("Restaurant_Phone");
            entity.Property(e => e.RestaurantReservationCount).HasColumnName("Restaurant_ReservationCount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
