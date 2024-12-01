using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class StorageProductDTO
    {
        public int StorageId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class StorageProductDetailsDTO
    {
        public int StorageId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
