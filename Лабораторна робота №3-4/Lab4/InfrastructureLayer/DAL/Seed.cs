using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DAL
{
    public static class Seed
    {
        public static void SeedData(ProductsEnterpriseDBContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product { Name = "Hammer", Unit = "pcs", Price = 15.50m },
                    new Product { Name = "Drill", Unit = "pcs", Price = 80.00m },
                    new Product { Name = "Screwdriver", Unit = "pcs", Price = 10.00m }
                };
                context.Products.AddRange(products);
            }

            if (!context.Storages.Any())
            {
                var storages = new List<Storage>
                {
                    new Storage { Name = "Main Warehouse", Region = "Central", City = "Kyiv", Address = "123 Main St" },
                    new Storage { Name = "Regional Warehouse", Region = "West", City = "Lviv", Address = "45 Industrial Rd" }
                };
                context.Storages.AddRange(storages);
            }

            if (!context.StorageKeepers.Any())
            {
                var storageKeepers = new List<StorageKeeper>
                {
                    new StorageKeeper { StorageId = 3, FirstName = "Ivan", LastName = "Petrenko", Phone = "1234567890" },
                    new StorageKeeper { StorageId = 4, FirstName = "Olena", LastName = "Kovalenko", Phone = "0987654321" }
                };
                context.StorageKeepers.AddRange(storageKeepers);
            }

            if (!context.StorageProducts.Any())
            {
                var storageProducts = new List<StorageProduct>
                {
                    new StorageProduct { StorageId = 3, ProductId = 4, Quantity = 50 },
                    new StorageProduct { StorageId = 3, ProductId = 5, Quantity = 20 },
                    new StorageProduct { StorageId = 4, ProductId = 6, Quantity = 100 }
                };
                context.StorageProducts.AddRange(storageProducts);
            }

            context.SaveChanges();
        }
    }
}
