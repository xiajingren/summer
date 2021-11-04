using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Summer.Application.Behaviors;
using Summer.Application.Interfaces;
using Summer.Application.Options;
using Summer.Application.UnitOfWork;
using Summer.Domain.Interfaces;
using Summer.Domain.Options;
using Summer.Domain.SeedWork;
using Summer.Domain.Services;
using Summer.Infrastructure.Data;
using Summer.Infrastructure.Data.UnitOfWork;
using Summer.Infrastructure.Extensions;
using Summer.Infrastructure.HttpFilters;
using Summer.Infrastructure.MasterData;
using Summer.Infrastructure.Services;

namespace Summer.Infrastructure
{
    public static class ApplicationBootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            AddMvc(services, configuration);

            AddCoreServices(services);

            AddJwtAuthentication(services, configuration);

            AddSwagger(services);

            AddDbContext(services);
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

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            DbContextSeed(app);
        }

        #region Mvc

        private static void AddMvc(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(options => { options.Filters.Add<HttpGlobalExceptionFilter>(); });
            // .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            // .AddFluentValidation(mvcConfiguration =>
            // {
            //     mvcConfiguration.RegisterValidatorsFromAssemblyContaining(typeof(ApplicationBootstrapper));
            // });

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    // 是否支持自定义ModelState验证失败过滤器  默认false 返回 400 - ValidationProblemDetails
            //    options.SuppressModelStateInvalidFilter = true;
            //});

            //services.AddSpaStaticFiles(c => { c.RootPath = "ClientApp/dist"; });

            // services.Configure<RouteOptions>(options =>
            // {
            //     options.LowercaseUrls = true;
            //     options.LowercaseQueryStrings = true;
            // });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var origins = configuration.GetSection("AllowedOrigins").Value
                            .Split(";", StringSplitOptions.RemoveEmptyEntries);

                        builder.WithOrigins(origins)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        #endregion

        #region Authentication

        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection(nameof(JwtOptions));
            var jwtOptions = jwtConfig.Get<JwtOptions>();

            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            TokenValidationParameters GenerateTokenValidationParameters() =>
                new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
                };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => { options.TokenValidationParameters = GenerateTokenValidationParameters(); });

            var tokenValidationParameters = GenerateTokenValidationParameters();
            tokenValidationParameters.ValidateLifetime = false; //refresh token验证jwt正确性时使用，忽略过期时间
            services.AddSingleton(tokenValidationParameters);

            services.Configure<JwtOptions>(jwtConfig);
        }

        #endregion

        #region Core Services

        private static void AddCoreServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ICurrentUser).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CheckPermissionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            services.AddAutoMapper(typeof(ICurrentUser).Assembly);

            services.AddValidatorsFromAssembly(typeof(ICurrentUser).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IPermissionManager, PermissionManager>();
            services.AddTransient<ITenantManager, TenantManager>();
            services.AddTransient<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICurrentTenant, CurrentTenant>();

            services.Configure<UserOptions>(options =>
            {
                options.PasswordRequireDigit = false;
                options.PasswordRequireLowercase = false;
                options.PasswordRequireUppercase = false;
            });
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

                c.DescribeAllParametersInCamelCase();
            });
        }

        #endregion

        #region DbContext

        private static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContextWithDefaultRepository<MasterDbContext>();
            services.AddDbContextWithDefaultRepository<SummerDbContext>();

            services.AddTransient<IDataSeed, MasterDbSeed>();
            services.AddTransient<IDataSeed, SummerDbSeed>();
        }

        private static void DbContextSeed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var seeds = scope.ServiceProvider.GetServices<IDataSeed>();
            foreach (var seed in seeds) seed.SeedAsync().Wait();
        }

        #endregion
    }
}