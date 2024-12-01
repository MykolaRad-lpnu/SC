using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProductsEnterpriseOrganizer.UI.Views;

namespace ProductsEnterpriseOrganizer.UI.ViewModels
{
    public class DataViewModel : ViewModelBase
    {
        public DataViewModel()
        {
            SetControlVisibility = new Command(ControlVisibility);
            ViewProductsInStorageCommand = new Command(ViewProductsInStorage);
            ViewKeepersInStorageCommand = new Command(ViewKeepersInStorage);
        }

        private string _visibleControl = "Storages";
        public string VisibleControl
        {
            get
            {
                return _visibleControl;
            }
            set
            {
                _visibleControl = value;
                OnPropertyChanged("VisibleControl");
            }
        }

        public ICommand SetControlVisibility { get; set; }

        public void ControlVisibility(object args)
        {
            SaveElementsFromStorage();
            VisibleControl = args.ToString();
        }

        public ICommand ViewProductsInStorageCommand { get; set; }

        public void ViewProductsInStorage(object args)
        {
            CurrentProducts = SelectedStorage.Products;
            VisibleControl = "Products";
        }

        public ICommand ViewKeepersInStorageCommand { get; set; }
        public void ViewKeepersInStorage(object args)
        {
            CurrentKeepers = SelectedStorage.StorageKeepers;
            VisibleControl = "StorageKeepers";
        }

        public void SaveElementsFromStorage()
        {
            Products = new ObservableCollection<ProductViewModel>(Storages.SelectMany(storage => storage.Products));
            StorageKeepers = new ObservableCollection<StorageKeeperViewModel>(Storages.SelectMany(storage => storage.StorageKeepers));

            CurrentProducts = Products;
            CurrentKeepers = StorageKeepers;
        }

        private ObservableCollection<StorageViewModel> _storages;
        public ObservableCollection<StorageViewModel> Storages
        {
            get
            {
                return _storages;
            }
            set
            {
                _storages = value;
                OnPropertyChanged("Storages");
            }
        }

        private StorageViewModel _selectedStorage;
        public StorageViewModel SelectedStorage
        {
            get => _selectedStorage;
            set
            {
                _selectedStorage = value;
                OnPropertyChanged("SelectedStorage");
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

        private ObservableCollection<ProductViewModel> _currentProducts;
        public ObservableCollection<ProductViewModel> CurrentProducts
        {
            get
            {
                return _currentProducts;
            }
            set
            {
                _currentProducts = value;
                OnPropertyChanged("CurrentProducts");
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

        private ObservableCollection<StorageKeeperViewModel> _currentKeepers;
        public ObservableCollection<StorageKeeperViewModel> CurrentKeepers
        {
            get
            {
                return _currentKeepers;
            }
            set
            {
                _currentKeepers = value;
                OnPropertyChanged("CurrentKeepers");
            }
        }
    }
}