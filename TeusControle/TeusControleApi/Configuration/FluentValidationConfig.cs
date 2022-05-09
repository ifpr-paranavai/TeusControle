using FluentValidation.AspNetCore;
using Manager.Mapping.User;
using Manager.Validator.User;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Globalization;

namespace TeusControleApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(p => {
                    p.RegisterValidatorsFromAssemblyContaining<CreateUsersValidator>();
                    p.RegisterValidatorsFromAssemblyContaining<UpdateUsersValidator>();
                    p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                });
        }
    }
}
