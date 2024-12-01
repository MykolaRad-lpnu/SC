using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DAL
{
    public partial class ProductsEnterpriseDBContext : DbContext
    {
        public ProductsEnterpriseDBContext()
        {
        }

        public ProductsEnterpriseDBContext(DbContextOptions<ProductsEnterpriseDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StorageKeeper> StorageKeepers { get; set; }

        public virtual DbSet<StorageProduct> StorageProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId); 
                entity.Property(p => p.Name)
                      .IsRequired() 
                      .HasMaxLength(100); 
                entity.HasIndex(p => p.Name)
                      .IsUnique(); 
                entity.Property(p => p.Unit)
                      .IsRequired()
                      .HasMaxLength(5);
                entity.Property(p => p.Price)
                      .IsRequired()
                      .HasColumnType("decimal(10,2)"); 
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.StorageId); 
                entity.Property(s => s.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasIndex(p => p.Name)
                      .IsUnique();
                entity.Property(s => s.Region)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(s => s.City)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(s => s.Address)
                      .IsRequired()
                      .HasMaxLength(200);
            });


            modelBuilder.Entity<StorageKeeper>(entity =>
            {
                entity.HasKey(k => k.StorageKeeperId);
                entity.Property(k => k.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(k => k.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(k => k.Phone)
                      .IsRequired()
                      .HasMaxLength(15);
                entity.HasIndex(k => k.Phone)
                      .IsUnique();
                entity.HasOne(k => k.Storage) 
                      .WithMany(s => s.StorageKeepers)
                      .HasForeignKey(k => k.StorageId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StorageProduct>(entity =>
            {
                entity.HasKey(sp => new { sp.StorageId, sp.ProductId });
                entity.Property(sp => sp.Quantity)
                      .IsRequired()
                      .HasDefaultValue(0);
                entity.HasOne(sp => sp.Storage)
                      .WithMany(s => s.StorageProducts)
                      .HasForeignKey(sp => sp.StorageId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(sp => sp.Product)
                      .WithMany(p => p.StorageProducts)
                      .HasForeignKey(sp => sp.ProductId)
                      .OnDelete(DeleteBehavior.Restrict); 
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
