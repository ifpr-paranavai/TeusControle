using AutoMapper;
using Core.Shared.Models.Product;
using System;

namespace Manager.Mapping.Product
{
    public class CreateProductMappingProfile : Profile
    {
        public CreateProductMappingProfile()
        {
            CreateMap<CreateProductModel, Core.Domain.Product>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
