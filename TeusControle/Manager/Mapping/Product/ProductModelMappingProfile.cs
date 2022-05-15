using AutoMapper;
using Core.Shared.Models.Product;

namespace Manager.Mapping.Product
{
    public class ProductModelMappingProfile : Profile
    {
        public ProductModelMappingProfile()
        {
            CreateMap<ProductModel, Core.Domain.Product>();

            CreateMap<Core.Domain.Product, ProductModel>();
        }
    }
}
