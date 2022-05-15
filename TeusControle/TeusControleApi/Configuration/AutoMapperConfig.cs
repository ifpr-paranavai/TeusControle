using Manager.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMappingProfile), typeof(UpdateUserMappingProfile), typeof(UserModelMappingProfile));
        }
    }
}
