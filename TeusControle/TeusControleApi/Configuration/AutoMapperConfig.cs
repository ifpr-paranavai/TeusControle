using Manager.Mapping.User;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CreateUserMappingProfile), typeof(UpdateUserMappingProfile));
        }
    }
}
