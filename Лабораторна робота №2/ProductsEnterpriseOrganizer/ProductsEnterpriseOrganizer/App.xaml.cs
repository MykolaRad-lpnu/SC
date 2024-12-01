using System.Configuration;
using System.Data;
using System.Windows;
using ProductEnterprizeOrganizer;
using ProductsEnterpriseOrganizer.UI.ViewModels;
using ProductsEnterpriseOrganizer.UI.Base;
using AutoMapper;

namespace ProductsEnterpriseOrganizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IMapper _mapper;
        private DataModel _model;
        private DataViewModel _viewModel;
        public App()
        {
            var mapping = new Mapping();
            mapping.Create();
            _mapper = mapping.GetMapper();

            _model = DataModel.Load();
            _viewModel = _mapper.Map<DataViewModel>(_model);
            new Mapping().Create();
            var window = new MainWindow() { DataContext = _viewModel };
            window.Show(); 
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _model = _mapper.Map<DataModel>(_viewModel);
                _model.Save();
            }
            catch (Exception)
            {
                base.OnExit(e);
                throw;
            }
        }
    }

}
