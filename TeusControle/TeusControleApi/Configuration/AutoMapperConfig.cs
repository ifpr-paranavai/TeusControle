using Manager.Mapping.Entry;
using Manager.Mapping.Product;
using Manager.Mapping.Sale;
using Manager.Mapping.User;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    /// <summary>
    /// Configuração de auto mapeamento
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Configuração de auto mapeamento
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(CreateUserMappingProfile), typeof(UpdateUserMappingProfile), typeof(UserModelMappingProfile),
                typeof(CreateProductMappingProfile), typeof(UpdateProductMappingProfile), typeof(ProductModelMappingProfile),
                typeof(CreateEntryMappingProfile), typeof(UpdateEntryMappingProfile), typeof(EntryModelMappingProfile),
                typeof(CreateSaleMappingProfile), typeof(UpdateSaleMappingProfile), typeof(SaleModelMappingProfile));
        }
    }
}
