using AutoMapper;
using Organization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.UI.ViewModels;
using Task = Organization.Model.Task;

namespace Organizer.UI.Base
{
    public class Mapping
    {
        private static readonly Mapping _instance = new Mapping();
        private readonly IMapper _mapper;

        private Mapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataModel, DataViewModel>();
                cfg.CreateMap<DataViewModel, DataModel>();

                cfg.CreateMap<Task, TaskViewModel>();
                cfg.CreateMap<TaskViewModel, Task>();

                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<ProjectViewModel, Project>();

                cfg.CreateMap<Event, EventViewModel>();
                cfg.CreateMap<EventViewModel, Event>();
            });

            _mapper = config.CreateMapper();
        }

        public static Mapping Instance => _instance;

        public DataViewModel MapToViewModel(DataModel dataModel)
        {
            return _mapper.Map<DataViewModel>(dataModel);
        }

        public DataModel MapToDataModel(DataViewModel dataViewModel)
        {
            return _mapper.Map<DataModel>(dataViewModel);
        }

        public TaskViewModel MapToViewModel(Task taskModel)
        {
            return _mapper.Map<TaskViewModel>(taskModel);
        }

        public Task MapToDataModel(TaskViewModel taskViewModel)
        {
            return _mapper.Map<Task>(taskViewModel);
        }

        public ProjectViewModel MapToViewModel(Project projectModel)
        {
            return _mapper.Map<ProjectViewModel>(projectModel);
        }

        public Project MapToDataModel(ProjectViewModel projectViewModel)
        {
            return _mapper.Map<Project>(projectViewModel);
        }

        public EventViewModel MapToViewModel(Event eventModel)
        {
            return _mapper.Map<EventViewModel>(eventModel);
        }

        public Event MapToDataModel(EventViewModel eventViewModel)
        {
            return _mapper.Map<Event>(eventViewModel);
        }
    }
}
