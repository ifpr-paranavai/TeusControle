using AutoMapper;
using Core.Domain;
using Core.Shared.Models.User;
using System;

namespace Manager.Mapping.User
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<CreateUserModel, Users>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));
        }
    }
}
