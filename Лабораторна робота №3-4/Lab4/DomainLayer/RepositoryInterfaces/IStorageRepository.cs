using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepositoryInterfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAllStoragesAsync();
        Task<Storage> GetStorageByIdAsync(int id);
        Task<bool> StorageExistsAsync(int storageId);
        Task<bool> StorageExistsAsync(string region, string country, string address);
        Task<bool> StorageExistsAsync(string name);
        Task<bool> CreateStorageAsync(Storage storage);
        Task<bool> UpdateStorageAsync(Storage storage);
        Task<bool> DeleteStorageAsync(Storage storage);
        Task<bool> SaveAsync();
    }
}
