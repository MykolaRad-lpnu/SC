using Organization.Model.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Model
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public IEnumerable<Task> Tasks { get; set; }

        [DataMember]
        public IEnumerable<Project> Projects { get; set; }

        [DataMember]
        public IEnumerable<Event> Events { get; set; }

        public DataModel() 
        {
            Tasks = new List<Task>() { new Task() { Name = "Enter new task here", Description = "Enter some description" } };
            Projects = new List<Project>() { new Project() };
            Events = new List<Event>() { new Event() };
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
