using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace TeusControleApi.Configuration
{
    /// <summary>
    /// Classe de configuração da base de dados
    /// </summary>
    public static class DataBaseConfig
    {
        /// <summary>
        /// Adiciona configuração para base de dados
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["DBHOST"] ?? "localhost";
            var port = configuration["DBPORT"] ?? "3306";
            var password = configuration["MYSQL_PASSWORD"] ?? configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = configuration["MYSQL_USER"] ?? configuration.GetConnectionString("MYSQL_USER");
            var productsdb = configuration["MYSQL_DATABASE"] ?? configuration.GetConnectionString("MYSQL_DATABASE");
            var options = configuration["MYSQL_OPTIONS"] ?? configuration.GetConnectionString("MYSQL_OPTIONS");

            string mySqlConnStr = $"server={host}; userid={userid};pwd={password};port={port};database={productsdb}; {options}";

            services.AddDbContext<MyContext>(options =>
            {
                options.UseMySql(
                    mySqlConnStr,
                    ServerVersion.AutoDetect(mySqlConnStr),
                    mysql =>
                    {
                        mysql.EnableRetryOnFailure();
                    }
                );
            });
        }

        /// <summary>
        /// Aplica configuração da base
        /// </summary>
        /// <param name="app"></param>
        public static void UseDataBaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<MyContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
