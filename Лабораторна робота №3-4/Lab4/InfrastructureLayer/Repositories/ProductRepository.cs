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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsEnterpriseDBContext _context;

        public ProductRepository(ProductsEnterpriseDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            return await SaveAsync();
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            return await SaveAsync();

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> ProductExistsAsync(int productId)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == productId);
        }

        public async Task<bool> ProductExistsAsync(string productName)
        {
            return await _context.Products.AnyAsync(p => p.Name == productName);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return await SaveAsync();
        }
    }
}
