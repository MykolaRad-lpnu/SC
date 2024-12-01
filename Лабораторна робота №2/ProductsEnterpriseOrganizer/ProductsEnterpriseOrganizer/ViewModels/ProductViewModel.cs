using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductsEnterpriseOrganizer.UI.ViewModels
{
    public class ProductViewModel : ViewModelBase
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

        private string _units;
        public string Units
        {
            get
            {
                return _units;
            }
            set
            {
                _units = value;
                OnPropertyChanged("Units");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        private uint _count;

        public uint Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }
    }
}