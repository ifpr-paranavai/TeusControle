using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeusControleApi.Configuration;

namespace TeusControleApi
{
    /// <summary>
    /// Classe de inicialização da aplicação
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Classe de inicialização da aplicação
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuração
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configuração de serviços
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
        /// Configura aplicação
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
