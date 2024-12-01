using ProductEnterprizeOrganizer;
using ProductsEnterpriseOrganizer.UI.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsEnterpriseOrganizer.UI.Base
{
    public class Mapping
    {
        private IMapper _mapper;

        public void Create()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataModel, DataViewModel>();
                cfg.CreateMap<DataViewModel, DataModel>();

                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();

                cfg.CreateMap<Storage, StorageViewModel>();
                cfg.CreateMap<StorageViewModel, Storage>();

                cfg.CreateMap<StorageKeeper, StorageKeeperViewModel>();
                cfg.CreateMap<StorageKeeperViewModel, StorageKeeper>();
            });

            _mapper = config.CreateMapper();
        }

        public IMapper GetMapper() => _mapper;
    }
}
