using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Storage
    {
        public int StorageId { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual ICollection<StorageProduct> StorageProducts { get; set; }
        
        public virtual ICollection<StorageKeeper> StorageKeepers { get; set; }
    }
}
