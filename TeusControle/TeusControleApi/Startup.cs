using Data.Context;
using Data.Repository;
using FluentValidation.AspNetCore;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Validator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Globalization;

namespace TeusControleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(p => {
                    p.RegisterValidatorsFromAssemblyContaining<UsersValidator>();
                    p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });

            services.AddDbContext<MyContext>(options => {
                options.UseMySql(
                    Configuration.GetConnectionString("MyConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))
                );
            });

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersManager, UsersManager>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Teus Controle Api",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Teus Controle V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
