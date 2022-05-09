﻿using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace TeusControleApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Teus Controle Api",
                    Version = "v1",
                    Description="API da aplicação Teus Controle",
                    Contact=new OpenApiContact
                    {
                        Name="Mateus Barbeiro Garcia",
                        Email="mateusgarcia2001@gmail.com",
                    },
                });

                /*c.AddFluentValidationRules();*/

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "Core.Shared.xml");
                c.IncludeXmlComments(xmlPath);
            });
            services.AddFluentValidationRulesToSwagger();
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Teus Controle V1");
            });
        }
    }
}
