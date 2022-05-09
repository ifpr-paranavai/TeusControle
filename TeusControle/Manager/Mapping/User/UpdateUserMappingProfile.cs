using AutoMapper;
using Core.Domain;
using Core.Shared.Models.User;
using System;

namespace Manager.Mapping.User
{
    public class UpdateUserMappingProfile : Profile
    {
        public UpdateUserMappingProfile()
        {
            CreateMap<UpdateUserModel, Users>()
                .ForMember(d => d.LastChange, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));
        }
    }
}
