using AutoMapper;
using Core.Shared.Models.Sale;
using System;

namespace Manager.Mapping.Sale
{
    public class CreateSaleMappingProfile : Profile
    {
        public CreateSaleMappingProfile()
        {
            CreateMap<CreateSaleModel, Core.Domain.Sale>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
