using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class StorageDTO
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class StorageDetailsDTO
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public List<StorageProductDetailsDTO> StorageProducts { get; set; }

        public List<StorageKeeperDetailsDTO> StorageKeepers { get; set; }
    }
}
