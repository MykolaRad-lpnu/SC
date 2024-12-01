using DomainLayer.Models;
using DomainLayer.RepositoryInterfaces;
using InfrastructureLayer.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    public class StorageProductRepository : IStorageProductRepository
    {
        private readonly ProductsEnterpriseDBContext _context;

        public StorageProductRepository(ProductsEnterpriseDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStorageProductAsync(StorageProduct product)
        {
            await _context.StorageProducts.AddAsync(product);

            return await SaveAsync();
        }

        public async Task<bool> DeleteStorageProductAsync(StorageProduct product)
        {
            _context.StorageProducts.Remove(product);
            return await SaveAsync();
        }

        public async Task<IEnumerable<StorageProduct>> GetAllStorageProductsAsync()
        {
            return await _context.StorageProducts
                .Include(sp => sp.Product) 
                .Include(sp => sp.Storage)
                .ToListAsync();
        }

        public async Task<StorageProduct> GetStorageProductByIdAsync(int productId, int storageId)
        {
            return await _context.StorageProducts
                .Include(sp => sp.Product) 
                .Include(sp => sp.Storage) 
                .FirstOrDefaultAsync(sp => sp.ProductId == productId && sp.StorageId == storageId); 
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> StorageProductExistsAsync(int productId, int storageId)
        {
            return await _context.StorageProducts.AnyAsync(sp => sp.ProductId == productId && sp.StorageId == storageId);
        }

        public async Task<bool> UpdateStorageProductAsync(StorageProduct product)
        {
            _context.StorageProducts.Update(product);
            return await SaveAsync();
        }

        public async Task<IEnumerable<StorageProduct>> GetStorageProductsByProductIdAsync(int productId)
        {
            return await _context.StorageProducts
                .Include(sp => sp.Product)
                .Where(sp => sp.ProductId == productId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StorageProduct>> GetStorageProductsByStorageIdAsync(int storageId)
        {
            return await _context.StorageProducts
                .Include(sp => sp.Storage) 
                .Where(sp => sp.StorageId == storageId)
                .ToListAsync();
        }
    }
}
