using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTO
{
    public class StorageKeeperDTO
    {
        public int StorageKeeperId { get; set; }

        public int StorageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }

    public class StorageKeeperDetailsDTO
    {
        public int StorageKeeperId { get; set; }
        public int StorageId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }
}
