using AutoMapper;
using Organization.Model;
using Organizer.UI.ViewModels;
using Organizer.UI.Base;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Organizer.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private DataModel _model;
        private DataViewModel _viewModel;
        public App()
        {
            _model = DataModel.Load();
            _viewModel = Mapping.Instance.MapToViewModel(_model);
            var window = new MainWindow() { DataContext = _viewModel };
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                _model = Mapping.Instance.MapToDataModel(_viewModel);
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
