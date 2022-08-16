using AutoMapper;
using Core.Shared.Models.Entry;
using System;

namespace Manager.Mapping.Entry
{
    public class UpdateEntryMappingProfile : Profile
    {
        public UpdateEntryMappingProfile()
        {
            CreateMap<UpdateEntryModel, Core.Domain.Entry>()
                .ForMember(d => d.LastChange, o => o.MapFrom(x => DateTime.Now));
        }
    }
}
