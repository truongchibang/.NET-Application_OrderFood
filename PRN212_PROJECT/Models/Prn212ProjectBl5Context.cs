using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PRN212_PROJECT.Models;

public partial class Prn212ProjectBl5Context : DbContext
{
    public Prn212ProjectBl5Context()
    {
    }

    public Prn212ProjectBl5Context(DbContextOptions<Prn212ProjectBl5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<AccRole> AccRoles { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodCart> FoodCarts { get; set; }

    public virtual DbSet<FoodOrder> FoodOrders { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TypeFood> TypeFoods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        var configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
    }
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=LAPTOP-E3K0NGUN\\SQLEXPRESS;uid=sa;password=sa;database=PRN212_PROJECT_BL5;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccRole>(entity =>
        {

            entity.HasKey(e => new { e.AccountId });
            entity.ToTable("Acc_Role");
                        entity.Property(e => e.AccountId)
                            .HasMaxLength(15)
                            .HasColumnName("account_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Account).WithMany().HasForeignKey(d => d.AccountId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_Acc_Role_Account");

            entity.HasOne(d => d.Role).WithMany().HasForeignKey(d => d.RoleId).OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_Acc_Role_Role");
        });
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.PhoneNumber);

            entity.ToTable("Account");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .HasColumnName("password");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(15)
                .HasColumnName("account_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Account");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.ToTable("Food");

            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.FoodName).HasColumnName("food_name");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TypefId).HasColumnName("typef_id");

            entity.HasOne(d => d.Typef).WithMany(p => p.Foods)
                .HasForeignKey(d => d.TypefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_Type_Food");
        });

        modelBuilder.Entity<FoodCart>(entity =>
        {
            entity.HasKey(e => e.FoodCart1);

            entity.ToTable("Food_cart");

            entity.Property(e => e.FoodCart1).HasColumnName("food_cart");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.FoodCarts)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_cart_Cart");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodCarts)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_cart_Food");
        });

        modelBuilder.Entity<FoodOrder>(entity =>
        {
            entity.HasKey(e => e.FoodOrder1);

            entity.ToTable("Food_order");

            entity.Property(e => e.FoodOrder1).HasColumnName("food_order");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodOrders)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_order_Food");

            entity.HasOne(d => d.Order).WithMany(p => p.FoodOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Food_order_Order");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AccountId)
                .HasMaxLength(15)
                .HasColumnName("account_id");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TimeFinish)
                .HasColumnType("datetime")
                .HasColumnName("Time_finish");
            entity.Property(e => e.TimeOder)
                .HasColumnType("datetime")
                .HasColumnName("Time_oder");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Comment).HasColumnName("comment");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Account");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Status");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.RoleDefine)
                .HasMaxLength(15)
                .HasColumnName("role_define");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TypeFood>(entity =>
        {
            entity.HasKey(e => e.TypefId);

            entity.ToTable("Type_Food");

            entity.Property(e => e.TypefId).HasColumnName("typef_id");
            entity.Property(e => e.Typename).HasColumnName("typename");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
