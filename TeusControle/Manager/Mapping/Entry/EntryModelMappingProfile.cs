using AutoMapper;
using Core.Shared.Models.Entry;

namespace Manager.Mapping.Entry
{
    public class EntryModelMappingProfile : Profile
    {
        public EntryModelMappingProfile()
        {
            CreateMap<EntryModel, Core.Domain.Entry>();

            CreateMap<Core.Domain.Entry, EntryModel>();
        }
    }
}
