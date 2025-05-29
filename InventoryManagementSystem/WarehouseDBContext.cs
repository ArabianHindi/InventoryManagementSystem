using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    class WarehouseDbContext : DbContext
    {
        private const string ConnectionString = @"Server=BluRay\MSSQLSERVER01;Database=WarehouseManagementDB;Trusted_Connection=True;MultipleActiveResultSets=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SupplyOrder> SupplyOrders { get; set; }
        public DbSet<SupplyOrderItem> SupplyOrderItems { get; set; }
        public DbSet<DisbursementOrder> DisbursementOrders { get; set; }
        public DbSet<DisbursementOrderItem> DisbursementOrderItems { get; set; }
        public DbSet<WarehouseInventory> WarehouseInventories { get; set; }
        public DbSet<TransferOrder> TransferOrders { get; set; }
        public DbSet<TransferOrderItem> TransferOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure unique constraints
            modelBuilder.Entity<Warehouse>()
                .HasIndex(w => w.WarehouseName)
                .IsUnique();

            modelBuilder.Entity<Item>()
                .HasIndex(i => i.ItemCode)
                .IsUnique();

            modelBuilder.Entity<SupplyOrder>()
                .HasIndex(so => so.OrderNumber)
                .IsUnique();

            modelBuilder.Entity<DisbursementOrder>()
                .HasIndex(d => d.OrderNumber)
                .IsUnique();

            // Configure composite unique constraint for WarehouseInventory
            modelBuilder.Entity<WarehouseInventory>()
                .HasIndex(wi => new { wi.WarehouseId, wi.ItemId, wi.SupplierId, wi.ProductionDate, wi.ExpiryDate })
                .IsUnique()
                .HasName("IX_WarehouseInventory_Unique");

            // Configure TransferOrder relationships
            modelBuilder.Entity<TransferOrder>()
                .HasOne(to => to.FromWarehouse)
                .WithMany(w => w.TransferOrdersFrom)
                .HasForeignKey(to => to.FromWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferOrder>()
                .HasOne(to => to.ToWarehouse)
                .WithMany(w => w.TransferOrdersTo)
                .HasForeignKey(to => to.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure automatic timestamp update for WarehouseInventory
            modelBuilder.Entity<WarehouseInventory>()
                .Property(wi => wi.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Add check constraint for TransferOrder (different warehouses)
            // Note: EF Core 3.1 doesn't support check constraints directly in model building
            // This would need to be added via raw SQL migration
        }
    }
}
