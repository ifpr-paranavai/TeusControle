using Manager.Mapping.Product;
using Manager.Mapping.User;
using Microsoft.Extensions.DependencyInjection;

namespace TeusControleApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(CreateUserMappingProfile), typeof(UpdateUserMappingProfile), typeof(UserModelMappingProfile),
                typeof(CreateProductMappingProfile), typeof(UpdateProductMappingProfile), typeof(ProductModelMappingProfile));
        }
    }
}
