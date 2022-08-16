using AutoMapper;
using Core.Shared.Models.Product;


namespace Manager.Mapping.Product
{
    public class ProductPagedModelMappingProfile : Profile
    {
        public ProductPagedModelMappingProfile()
        {
            CreateMap<ProductPagedModel, Core.Domain.Product>();

            CreateMap<Core.Domain.Product, ProductPagedModel>();
        }
    }
}
