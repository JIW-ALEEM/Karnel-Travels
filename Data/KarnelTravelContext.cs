using System;
using System.Collections.Generic;
using Karnel_Travels.Models;
using Microsoft.EntityFrameworkCore;

namespace Karnel_Travels.Data;

public partial class KarnelTravelContext : DbContext
{
    public KarnelTravelContext()
    {
    }

    public KarnelTravelContext(DbContextOptions<KarnelTravelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Resort> Resorts { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TouristSpot> TouristSpots { get; set; }

    public virtual DbSet<Travel> Travels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=KARNEL_TRAVEL;Persist Security Info=False;User ID=sa;Password=aptech;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD6B25FC5CE");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.FeedbackUserEmail, "UQ__Feedback__AE47C29C65F654CB").IsUnique();

            entity.Property(e => e.FeedbackMassage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackUserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FeedbackUserName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FeedbackUser).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.FeedbackUserId)
                .HasConstraintName("FK__Feedback__Feedba__4F7CD00D");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__Hotel__46023BDFE7CD55E1");

            entity.ToTable("Hotel");

            entity.Property(e => e.HotelDescription).HasColumnType("text");
            entity.Property(e => e.HotelImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HotelLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HotelName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HotelRooms)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Package__322035CC8413720D");

            entity.ToTable("Package");

            entity.Property(e => e.PackageDescription).HasColumnType("text");
            entity.Property(e => e.PackageImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PackagePerson)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PackageTour)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.PackageHotel).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageHotelId)
                .HasConstraintName("FK__Package__Package__3D5E1FD2");

            entity.HasOne(d => d.PackageResort).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageResortId)
                .HasConstraintName("FK__Package__Package__3F466844");

            entity.HasOne(d => d.PackageRestaurant).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageRestaurantId)
                .HasConstraintName("FK__Package__Package__3E52440B");

            entity.HasOne(d => d.PackageTouristSpot).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageTouristSpotId)
                .HasConstraintName("FK__Package__Package__3B75D760");

            entity.HasOne(d => d.PackageTravel).WithMany(p => p.Packages)
                .HasForeignKey(d => d.PackageTravelId)
                .HasConstraintName("FK__Package__Package__3C69FB99");
        });

        modelBuilder.Entity<Resort>(entity =>
        {
            entity.HasKey(e => e.ResortId).HasName("PK__Resort__7D2D740E88E8611A");

            entity.ToTable("Resort");

            entity.Property(e => e.ResortDescription).HasColumnType("text");
            entity.Property(e => e.ResortImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResortLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResortName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__Restaura__87454C95191C981E");

            entity.ToTable("Restaurant");

            entity.Property(e => e.RestaurantDescription).HasColumnType("text");
            entity.Property(e => e.RestaurantImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantMenu)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A66A3D18F");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TouristSpot>(entity =>
        {
            entity.HasKey(e => e.SpotId).HasName("PK__TouristS__61645F87A68B2412");

            entity.ToTable("TouristSpot");

            entity.Property(e => e.SpotDescription).HasColumnType("text");
            entity.Property(e => e.SpotImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SpotLocation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SpotName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Travel>(entity =>
        {
            entity.HasKey(e => e.TravelId).HasName("PK__Travel__E9315235783801F8");

            entity.ToTable("Travel");

            entity.Property(e => e.TravelDescription).HasColumnType("text");
            entity.Property(e => e.TravelImage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TravelMode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C63BB763D");

            entity.HasIndex(e => e.UserEmail, "UQ__Users__08638DF803070A96").IsUnique();

            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK__Users__UserRoleI__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
