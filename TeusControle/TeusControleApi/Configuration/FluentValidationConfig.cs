using FluentValidation.AspNetCore;
using Manager.Validator.Entry;
using Manager.Validator.Product;
using Manager.Validator.User;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using System.Text.Json.Serialization;

namespace TeusControleApi.Configuration
{
    /// <summary>
    /// Classe de configuração do validador
    /// </summary>
    public static class FluentValidationConfig
    {
        /// <summary>
        /// Adiciona validadores para objetos de entrada
        /// </summary>
        /// <param name="services"></param>
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x => {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.Converters.Add(new StringEnumConverter());
                })
                .AddJsonOptions(p => {
                    p.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                })
                .AddFluentValidation(p => {
                    p.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<UpdateUserValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<UpdateProductValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<CreateEntryValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<UpdateEntryValidator>();
                    p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });
        }
    }
}
