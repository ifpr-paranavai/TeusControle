using AutoMapper;
using Core.Shared.Models.Sale;

namespace Manager.Mapping.Sale
{
    public class SaleModelMappingProfile : Profile
    {
        public SaleModelMappingProfile()
        {
            CreateMap<SaleModel, Core.Domain.Sale>();

            CreateMap<Core.Domain.Sale, SaleModel>();
        }
    }
}
