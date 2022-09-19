using AutoMapper;
using Core.Shared.Models.Sale;
using System;

namespace Manager.Mapping.Sale
{
    public class UpdateSaleMappingProfile : Profile
    {
        public UpdateSaleMappingProfile()
        {
            CreateMap<UpdateSaleModel, Core.Domain.Sale>()
                .ForMember(d => d.LastChange, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
