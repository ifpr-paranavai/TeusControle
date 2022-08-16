using AutoMapper;
using Core.Shared.Models.Entry;

namespace Manager.Mapping.Entry
{
    public class EntryPagedModelMappingProfile : Profile
    {
        public EntryPagedModelMappingProfile()
        {
            CreateMap<EntryPagedModel, Core.Domain.Entry>();

            CreateMap<Core.Domain.Entry, EntryPagedModel>();
        }
    }
}
