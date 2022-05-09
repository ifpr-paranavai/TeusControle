using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TeusControleApi.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyContext>(options => {
                options.UseMySql(
                    configuration.GetConnectionString("MyConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))
                );
            });
        }

        public static void UseDataBaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MyContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
