using Data.Repository;
using Data.Repository.Base;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Interfaces.Repositories;
using Manager.Interfaces.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    /// <summary>
    /// Classe de configuração de injeção de dependência
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Adiciona configuração de injeção
        /// </summary>
        /// <param name="services"></param>
        public static void UseDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region [Repository]  
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseDoubleRepository<>), typeof(BaseDoubleRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductEntryRepository, ProductEntryRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
            #endregion

            #region [Manager]
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IEntryManager, EntryManager>();
            services.AddScoped<IProductEntryManager, ProductEntryManager>();
            services.AddScoped<ISelectManager, SelectManager>();
            services.AddScoped<ISaleManager, SaleManager>();
            services.AddScoped<IProductSaleManager, ProductSaleManager>();
            #endregion
        }
    }
}
