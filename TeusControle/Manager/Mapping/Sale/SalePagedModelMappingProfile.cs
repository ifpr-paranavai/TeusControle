using AutoMapper;
using Core.Shared.Models.Sale;

namespace Manager.Mapping.Sale
{
    public class SalePagedModelMappingProfile : Profile
    {
        public SalePagedModelMappingProfile()
        {
            CreateMap<SalePagedModel, Core.Domain.Sale>();

            CreateMap<Core.Domain.Sale, SalePagedModel>();
        }
    }
}
