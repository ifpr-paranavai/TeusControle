using AutoMapper;
using Core.Shared.Models.User;


namespace Manager.Mapping.User
{
    public class UserModelMappingProfile : Profile
    {
        public UserModelMappingProfile()
        {
            CreateMap<UserModel, Core.Domain.User>()
                .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Date));

            CreateMap<Core.Domain.User, UserModel>();
        }
    }
}
