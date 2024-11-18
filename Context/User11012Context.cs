using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo1112remake.Models;

namespace demo1112remake.Context;

public partial class User11012Context : DbContext
{
    public User11012Context()
    {
    }

    public User11012Context(DbContextOptions<User11012Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cathegory> Cathegories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PunktiVidachi> PunktiVidachis { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Satu> Satus { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.7.159:5432;Database=user11012;Username=user11012;Password=22067");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cathegory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cathegory_pk");

            entity.ToTable("cathegory", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("manufacturer_pk");

            entity.ToTable("manufacturer", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pk");

            entity.ToTable("order", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Client).HasColumnName("client");
            entity.Property(e => e.ClientFio)
                .HasColumnType("character varying")
                .HasColumnName("client_fio");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.DateDelivery).HasColumnName("date_delivery");
            entity.Property(e => e.DateForming).HasColumnName("date_forming");
            entity.Property(e => e.PunktVidachi).HasColumnName("punkt_vidachi");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ClientNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Client)
                .HasConstraintName("order_user_fk");

            entity.HasOne(d => d.PunktVidachiNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PunktVidachi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_punkti_vidachi_fk");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_satus_fk");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_product", "demo1112epic_sequel");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.IdOrderNavigation).WithMany()
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_product_order_fk");

            entity.HasOne(d => d.IdProductNavigation).WithMany()
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_product_product_fk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pk");

            entity.ToTable("product", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ActualDiscount).HasColumnName("actual_discount");
            entity.Property(e => e.Articul)
                .HasColumnType("character varying")
                .HasColumnName("articul");
            entity.Property(e => e.Cathegory).HasColumnName("cathegory");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("character varying")
                .HasColumnName("image");
            entity.Property(e => e.Manufacturer).HasColumnName("manufacturer");
            entity.Property(e => e.MaxDiscount).HasColumnName("max_discount");
            entity.Property(e => e.Measure)
                .HasColumnType("character varying")
                .HasColumnName("measure");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.QuantityStored).HasColumnName("quantity_stored");
            entity.Property(e => e.Supplier).HasColumnName("supplier");

            entity.HasOne(d => d.CathegoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Cathegory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_cathegory_fk");

            entity.HasOne(d => d.ManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_manufacturer_fk");

            entity.HasOne(d => d.SupplierNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.Supplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_supplier_fk");
        });

        modelBuilder.Entity<PunktiVidachi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("punkti_vidachi_pk");

            entity.ToTable("punkti_vidachi", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pk");

            entity.ToTable("roles", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Satu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("satus_pk");

            entity.ToTable("satus", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supplier_pk");

            entity.ToTable("supplier", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("user", "demo1112epic_sequel");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_roles_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
