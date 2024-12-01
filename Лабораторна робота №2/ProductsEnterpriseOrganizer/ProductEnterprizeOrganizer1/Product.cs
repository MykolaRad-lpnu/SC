using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductEnterprizeOrganizer
{
    [DataContract]
    public class Product
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Units { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public uint Count { get; set; }
    }
}


/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductEnterprizeOrganizer
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public uint StorageId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Units { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public double Count { get; set; }
    }
}*/
