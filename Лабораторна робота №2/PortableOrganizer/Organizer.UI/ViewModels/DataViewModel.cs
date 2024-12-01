using Organization.Model;
using Task = Organization.Model.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Organizer.UI.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        private ObservableCollection<TaskViewModel> _tasks;
        public ObservableCollection<TaskViewModel> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        private ObservableCollection<ProjectViewModel> _projects;
        public ObservableCollection<ProjectViewModel> Projects 
        { 
            get
            {
                return _projects;
            }
            set
            {
                _projects = value;
                OnPropertyChanged("Projects");
            }
        }

        private ObservableCollection<EventViewModel> _events;
        public ObservableCollection<EventViewModel> Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                OnPropertyChanged("Events");
            }
        }
    }
}
