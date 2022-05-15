using AutoMapper;
using Core.Shared.Models.User;
using System;

namespace Manager.Mapping.User
{
    public class UpdateUserMappingProfile : Profile
    {
        public UpdateUserMappingProfile()
        {
            CreateMap<UpdateUserModel, Core.Domain.User>()
                .ForMember(d => d.LastChange, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));
        }
    }
}
