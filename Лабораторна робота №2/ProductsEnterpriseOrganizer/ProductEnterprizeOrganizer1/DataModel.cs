using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProductEnterprizeOrganizer.Serialization;

namespace ProductEnterprizeOrganizer
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public IEnumerable<Storage> Storages { get; set; }

        [DataMember]
        public IEnumerable<Product> Products { get; set; }

        [DataMember]
        public IEnumerable<StorageKeeper> storageKeepers { get; set; }

        public DataModel()
        {
            Storages = new List<Storage>() { new Storage() { Name = "Storage 1", Location = "Location 1" } };
            Products = new List<Product>();
            storageKeepers = new List<StorageKeeper>();
        }
        public static string DataPath = "organizer.dat";

        public static DataModel Load()
        {
            if (File.Exists(DataPath))
                return DataSerializer.DeserializeItem(DataPath);

            return new DataModel();
        }

        public void Save()
        {
            DataSerializer.SerializeData(DataPath, this);
        }
    }
}
