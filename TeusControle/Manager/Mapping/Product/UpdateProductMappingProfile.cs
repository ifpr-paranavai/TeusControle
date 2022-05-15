using AutoMapper;
using Core.Shared.Models.Product;
using System;

namespace Manager.Mapping.Product
{
    public class UpdateProductMappingProfile : Profile
    {
        public UpdateProductMappingProfile()
        {
            CreateMap<UpdateProductModel, Core.Domain.Product>()
                .ForMember(d => d.LastChange, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
