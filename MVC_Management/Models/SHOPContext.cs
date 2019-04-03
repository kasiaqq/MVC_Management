using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVC_Management.Models
{
    public partial class SHOPContext : DbContext
    {
        public SHOPContext()
        {
        }

        public SHOPContext(DbContextOptions<SHOPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SHOP;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.BuildingNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("E-mail")
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FlatNumber).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.Property(e => e.Delivery1)
                    .IsRequired()
                    .HasColumnName("Delivery")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Clients");

                entity.HasOne(d => d.DeliveryNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Delivery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Delivery");

                entity.HasOne(d => d.PaymentNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Payment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Payment");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Payment1)
                    .IsRequired()
                    .HasColumnName("Payment")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ImgUrl).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Products");
            });
        }
    }
}
