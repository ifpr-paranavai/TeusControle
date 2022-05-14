using Data.Repository;
using Data.Repository.Base;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Interfaces.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void UseDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region [Repository]  
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseDoubleRepository<>), typeof(BaseDoubleRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region [Manager]
            services.AddScoped<IUserManager, UserManager>();
            #endregion
        }
    }
}
