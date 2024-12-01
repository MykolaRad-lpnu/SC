using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTO;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDetailsDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Storage, StorageDetailsDTO>();
            CreateMap<StorageDTO, Storage>();

            CreateMap<StorageProduct, StorageProductDetailsDTO>();
            CreateMap<StorageProductDTO, StorageProduct>();

            CreateMap<StorageKeeper, StorageKeeperDetailsDTO>();
            CreateMap<StorageKeeperDTO, StorageKeeper>();
        }
    }
}
