using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Summer.Infra.Data.SeedWork;

namespace Summer.Infra.Bootstrapper
{
    public static class ApplicationBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSpaStaticFiles(c => { c.RootPath = "ClientApp/dist"; });

            AddSwagger(services);

            AddDbContextSeed(services);
        }

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                UseSwagger(app);
            }

            app.UseSpaStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            DbContextSeed(app);
        }

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
                        new string[] { }
                    }
                });
            });
        }

        private static void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Summer.WebApi v1"); });
        }

        #endregion

        #region DbContextSeed

        private static void AddDbContextSeed(IServiceCollection services)
        {
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