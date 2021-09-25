using System;
using System.IO;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Summer.Application.HttpFilters;
using Summer.Application.Options;
using Summer.Domain.Entities;
using Summer.Infra.Data;
using Summer.Infra.Data.DataSeeds;

namespace Summer.Application
{
    public static class ApplicationBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            AddMvc(services);

            services.AddMediatR(typeof(ApplicationBootstrapper));

            AddIdentity(services, configuration);

            AddJwtAuthentication(services, configuration);

            AddSwagger(services);

            AddDbContextSeed(services);
        }

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Summer.WebApi v1"); });
            }

            //app.UseSpaStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            DbContextSeed(app);
        }

        #region Mvc

        private static void AddMvc(IServiceCollection services)
        {
            services
                .AddControllers(options => { options.Filters.Add<HttpGlobalExceptionFilter>(); })
                .AddFluentValidation(mvcConfiguration =>
                {
                    mvcConfiguration.RegisterValidatorsFromAssemblyContaining(typeof(ApplicationBootstrapper));
                });

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    // 是否支持自定义ModelState验证失败过滤器  默认false 返回 400 - ValidationProblemDetails
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            //services.AddSpaStaticFiles(c => { c.RootPath = "ClientApp/dist"; });
        }

        #endregion

        #region Authentication

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection(nameof(JwtOptions));
            var jwtOptions = jwtConfig.Get<JwtOptions>();
            services.Configure<JwtOptions>(jwtConfig);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
            };

            services.AddSingleton(tokenValidationParameters);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    //options.SaveToken = true;
                    options.TokenValidationParameters = tokenValidationParameters;
                });
        }

        #endregion

        #region Identity

        private static void AddIdentity(IServiceCollection services, IConfiguration configuration)
        {
            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                    sqliteOptions => sqliteOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentityCore<ApplicationUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        #endregion

        #region Swagger

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Summer.WebApi",
                    Version = "v1",
                    Description = "Summer.WebApi Swagger Doc"
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        #endregion

        #region DbContextSeed

        private static void AddDbContextSeed(IServiceCollection services)
        {
            services.AddScoped<IDbContextSeed, ApplicationDbContextSeed>();
        }

        private static void DbContextSeed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var seeds = scope.ServiceProvider.GetServices<IDbContextSeed>();
            foreach (var seed in seeds) seed.SeedAsync().Wait();
        }

        #endregion
    }
}