using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepositoryInterfaces
{
    public interface IStorageKeeperRepository
    {
        Task<IEnumerable<StorageKeeper>> GetAllKeepersAsync();
        Task<StorageKeeper> GetKeeperByIdAsync(int id);
        Task<bool> KeeperExistsAsync(int keeperId);
        Task<bool> KeeperExistsAsync(string keeperPhone);
        Task<bool> CreateKeeperAsync(StorageKeeper keeper);
        Task<bool> UpdateKeeperAsync(StorageKeeper keeper);
        Task<IEnumerable<StorageKeeper>> GetKeepersByStorageIdAsync(int storageId);
        Task<bool> DeleteKeeperAsync(StorageKeeper keeper);
        Task<bool> SaveAsync();
    }
}
