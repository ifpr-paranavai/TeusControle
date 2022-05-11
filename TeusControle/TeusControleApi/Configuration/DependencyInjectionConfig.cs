using Data.Repository;
using Manager.Implementation;
using Manager.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void UseDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
        }
    }
}
