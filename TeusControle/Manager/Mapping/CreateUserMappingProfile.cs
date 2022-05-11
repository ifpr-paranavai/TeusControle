using AutoMapper;
using Core.Domain;
using Core.Shared.Models.User;
using System;

namespace Manager.Mapping
{
    public class CreateUserMappingProfile : Profile
    {
        public CreateUserMappingProfile()
        {
            CreateMap<CreateUserModel, User>()
                .ForMember(d => d.CreatedDate, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));
        }
    }
}
