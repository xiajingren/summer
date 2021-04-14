using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Summer.Core.Common
{
    public static class SummerServiceCollectionExtensions
    {
        public static IServiceCollection AddSummer(this IServiceCollection services, Action<SummerOptions> optionsAction = null)
        {
            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    BearerFormat = "JWT",
                //    Scheme = "Bearer"
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme{
                //            Reference = new OpenApiReference {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"}
                //        },new string[] { }
                //    }
                //});
            });

            if (optionsAction != null)
                services.Configure(optionsAction);

            return services;
        }
    }
}
