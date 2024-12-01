using ProductEnterprizeOrganizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductsEnterpriseOrganizer.UI.ViewModels
{
    public class StorageViewModel : ViewModelBase
    {
        public StorageViewModel() 
        {
            _products = new ObservableCollection<ProductViewModel>();
            _storageKeepers = new ObservableCollection<StorageKeeperViewModel>();
        }
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

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        private ObservableCollection<ProductViewModel> _products;
        public ObservableCollection<ProductViewModel> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }

        private ObservableCollection<StorageKeeperViewModel> _storageKeepers;
        public ObservableCollection<StorageKeeperViewModel> StorageKeepers
        {
            get
            {
                return _storageKeepers;
            }
            set
            {
                _storageKeepers = value;
                OnPropertyChanged("StorageKeepers");
            }
        }
    }
}