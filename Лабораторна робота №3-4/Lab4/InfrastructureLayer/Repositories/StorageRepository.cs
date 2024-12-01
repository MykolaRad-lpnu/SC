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
    public class StorageRepository : IStorageRepository
    {
        private readonly ProductsEnterpriseDBContext _context;

        public StorageRepository(ProductsEnterpriseDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStorageAsync(Storage storage)
        {
            await _context.Storages.AddAsync(storage);

            return await SaveAsync();
        }

        public async Task<bool> DeleteStorageAsync(Storage storage)
        {
            _context.Storages.Remove(storage);
            return await SaveAsync();
        }

        public async Task<IEnumerable<Storage>> GetAllStoragesAsync()
        {
            return await _context.Storages
                .Include(s => s.StorageProducts)
                .Include(s => s.StorageKeepers) 
                .ToListAsync(); 
        }

        public async Task<Storage> GetStorageByIdAsync(int id)
        {
            return await _context.Storages
                .Include(s => s.StorageProducts) 
                .Include(s => s.StorageKeepers) 
                .FirstOrDefaultAsync(s => s.StorageId == id);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> StorageExistsAsync(int storageId)
        {
            return await _context.Storages.AnyAsync(s => s.StorageId == storageId);
        }

        public async Task<bool> StorageExistsAsync(string region, string city, string address)
        {
            return await _context.Storages.AllAsync(s =>
                s.Region.Trim().ToUpper() == region.Trim().ToUpper() &&
                s.City.Trim().ToUpper() == city.ToUpper() &&
                s.Address.Trim().ToUpper() == address.Trim().ToUpper());
        }

        public async Task<bool> StorageExistsAsync(string name)
        {
            return await _context.Storages.AllAsync(s => s.Name.Trim().ToUpper() == name.Trim().ToUpper());
        }

        public async Task<bool> UpdateStorageAsync(Storage storage)
        {
            _context.Storages.Update(storage);
            return await SaveAsync();
        }
    }
}
