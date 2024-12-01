using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RepositoryInterfaces
{
    public interface IStorageProductRepository
    {
        Task<IEnumerable<StorageProduct>> GetAllStorageProductsAsync();
        Task<StorageProduct> GetStorageProductByIdAsync(int productId, int storageId);
        Task<bool> StorageProductExistsAsync(int productId, int storageId);
        Task<bool> CreateStorageProductAsync(StorageProduct product);
        Task<bool> UpdateStorageProductAsync(StorageProduct product);
        Task<bool> DeleteStorageProductAsync(StorageProduct product);
        Task<IEnumerable<StorageProduct>> GetStorageProductsByStorageIdAsync(int storageId);
        Task<IEnumerable<StorageProduct>> GetStorageProductsByProductIdAsync(int productId);
        Task<bool> SaveAsync();
    }
}
