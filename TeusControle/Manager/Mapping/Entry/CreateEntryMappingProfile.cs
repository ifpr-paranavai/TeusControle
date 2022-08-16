using AutoMapper;
using Core.Shared.Models.Entry;
using System;

namespace Manager.Mapping.Entry
{
    public class CreateEntryMappingProfile : Profile
    {
        public CreateEntryMappingProfile()
        {
            CreateMap<CreateEntryModel, Core.Domain.Entry>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
