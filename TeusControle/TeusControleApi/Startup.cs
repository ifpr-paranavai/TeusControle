using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeusControleApi.Configuration;

namespace TeusControleApi
{
    /// <summary>
    /// Classe de inicializa��o da aplica��o
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Classe de inicializa��o da aplica��o
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configura��o
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configura��o de servi�os
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddJwtConfiguration(Configuration);

            services.AddFluentValidationConfiguration();

            services.AddAutoMapperConfiguration();

            services.AddDataBaseConfiguration(Configuration);

            services.UseDependencyInjectionConfiguration();

            services.AddSwaggerConfiguration();
        }

        /// <summary>
        /// Configura aplica��o
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/error");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDataBaseConfiguration();

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseJwtConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
