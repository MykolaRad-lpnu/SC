using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductEnterprizeOrganizer
{
    [DataContract]
    public class Storage
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public IEnumerable<Product> Products { get; set; }

        [DataMember]
        public IEnumerable<StorageKeeper> StorageKeepers { get; set; }

    }
}