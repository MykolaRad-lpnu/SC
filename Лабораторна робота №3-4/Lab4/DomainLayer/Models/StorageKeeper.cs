using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class StorageKeeper
    {
        public int StorageKeeperId { get; set; }
        public int StorageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public virtual Storage Storage { get; set; }
    }
}
