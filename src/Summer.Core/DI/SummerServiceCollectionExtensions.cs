using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Summer.App.DI;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Summer.Core.Jwt;

namespace Summer.Core.DI
{
    public static class SummerServiceCollectionExtensions
    {
        public static IServiceCollection AddSummer(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            var summerOptions = services.Configure<SummerOptions>(configuration.GetSection("SummerOptions"))
                .BuildServiceProvider().GetRequiredService<IOptions<SummerOptions>>().Value;

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(c =>
            {
                c.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Summer API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"}
                        },new string[] { }
                    }
                });
            });

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = summerOptions.JwtOptions.Issuer,
                        ValidAudience = summerOptions.JwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(summerOptions.JwtOptions.SecurityKey)),
                    };
                });

            services.AddSingleton<IJwtTokenHelper, JwtTokenHelper>();

            services.AddSummerDbContext(summerOptions.ConnectionString);
            services.AddSummerService();

            return services;
        }
    }
}
