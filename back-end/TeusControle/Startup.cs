using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Application.Services;
using TeusControle.Domain.Models;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Context;
using TeusControle.Infrastructure.Repository;

namespace TeusControle
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors();

            // Serviços
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEntriesService, EntriesService>();
            services.AddScoped<IProductEntriesService, ProductEntriesService>();
            services.AddScoped<IDisposalsService, DisposalsService>();
            services.AddScoped<IProductDisposalsService, ProductDisposalsService>();

            // Repositórios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseDoubleRepository<>), typeof(BaseDoubleRepository<>));
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEntriesRepository, EntriesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductEntriesRepository, ProductEntriesRepository>();
            services.AddScoped<IDisposalsRepository, DisposalsRepository>();
            services.AddScoped<IProductDisposalsRepository, ProductDisposalsRepository>();

            // Mapeamento
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserModel, Users>();
                config.CreateMap<UpdateUserModel, Users>();
                config.CreateMap<Users, UserModel>();
                config.CreateMap<CreateProductsModel, Products>();
                config.CreateMap<UpdateProductsModel, Products>();
                config.CreateMap<Products, ProductsModel>();
                config.CreateMap<Entries, Entries>();
                config.CreateMap<CreateProductEntriesModel, ProductEntries>();
                config.CreateMap<UpdateEntryModel, ProductEntries>();
                config.CreateMap<ProductEntries, ProductEntriesModel>();
                config.CreateMap<CreateEntryModel, Entries>();
                config.CreateMap<UpdateEntryModel, Entries>();
                config.CreateMap<Entries, EntryModel>();
                config.CreateMap<CreateProductDisposalsModel, ProductDisposals>();
                config.CreateMap<UpdateProductDisposalsModel, ProductDisposals>();
                config.CreateMap<ProductDisposals, ProductDisposalsModel>();
                config.CreateMap<CreateDisposalModel, Disposals>();
                config.CreateMap<UpdateDisposalModel, Disposals>();
                config.CreateMap<Disposals, DisposalModel>();
            }).CreateMapper());

            // Configuração da base
            services.AddDbContext<ApiContext>(options => {
                options.UseMySql(
                    Configuration.GetConnectionString("MyConnection"),
                    new MySqlServerVersion(new Version(8, 0, 11))
                );
            });

            services.AddAuthentication(x => 
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Administrator",
                    policy => policy.RequireClaim("ProfileTypeId", "1")
                );
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.IncludeXmlComments(XmlCommentsFilePath);

                setup.AddSecurityDefinition(
                    jwtSecurityScheme.Reference.Id,
                    jwtSecurityScheme
                );

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { 
                        jwtSecurityScheme, 
                        Array.Empty<string>() 
                    }
                });
            });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
        )
        {
            if (env.EnvironmentName == EnvironmentName.Development)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}"
                );
            });
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "TeusControle API V1"
                );
            });
        }
    }
}
