using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Organization.Model.TaskStatus;

namespace Organizer.UI.ViewModels
{
    public class TaskViewModel : ViewModelBase
    {
        private string _name;

        public string Name
        { 
            get 
            { 
                return _name; 
            } 
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private TaskStatus _status;

        public TaskStatus Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
    }
}
