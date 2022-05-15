using AutoMapper;
using Core.Domain;
using Core.Shared.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Mapping
{
    public class UserModelMappingProfile : Profile
    {
        public UserModelMappingProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));

            CreateMap<User, UserModel>();
        }
    }
}
