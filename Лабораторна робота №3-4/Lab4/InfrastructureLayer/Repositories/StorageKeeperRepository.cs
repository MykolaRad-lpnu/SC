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
    public class StorageKeeperRepository : IStorageKeeperRepository
    {
        private readonly ProductsEnterpriseDBContext _context;

        public StorageKeeperRepository(ProductsEnterpriseDBContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateKeeperAsync(StorageKeeper keeper)
        {
            await _context.StorageKeepers.AddAsync(keeper);

            return await SaveAsync();
        }

        public async Task<bool> DeleteKeeperAsync(StorageKeeper keeper)
        {
            _context.StorageKeepers.Remove(keeper);
            return await SaveAsync();
        }

        public async Task<IEnumerable<StorageKeeper>> GetAllKeepersAsync()
        {
            return await _context.StorageKeepers
                .Include(k => k.Storage) 
                .ToListAsync(); 
        }

        public async Task<StorageKeeper> GetKeeperByIdAsync(int id)
        {
            return await _context.StorageKeepers
                .Include(k => k.Storage) 
                .FirstOrDefaultAsync(k => k.StorageKeeperId == id); 
        }

        public async Task<bool> KeeperExistsAsync(int keeperId)
        {
            return await _context.StorageKeepers.AnyAsync(sk => sk.StorageKeeperId == keeperId);
        }

        public async Task<bool> KeeperExistsAsync(string keeperPhone)
        {
            return await _context.StorageKeepers.AnyAsync(sk => sk.Phone == keeperPhone);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateKeeperAsync(StorageKeeper keeper)
        {
            _context.StorageKeepers.Update(keeper);
            return await SaveAsync();
        }

        public async Task<IEnumerable<StorageKeeper>> GetKeepersByStorageIdAsync(int storageId)
        {
            return await _context.StorageKeepers
                .Include(sp => sp.Storage)
                .Where(sp => sp.StorageId == storageId)
                .ToListAsync();
        }
    }
}
