using AutoMapper;
using Core.Shared.Models.User;


namespace Manager.Mapping.User
{
    public class UserPagedModelMappingProfile : Profile
    {
        public UserPagedModelMappingProfile()
        {
            CreateMap<UserPagedModel, Core.Domain.User>();

            CreateMap<Core.Domain.User, UserPagedModel>()
                 .ForMember(d => d.BirthDate, o => o.MapFrom(x => x.BirthDate.Value.ToString("dd/MM/yyyy")));
        }
    }
}
