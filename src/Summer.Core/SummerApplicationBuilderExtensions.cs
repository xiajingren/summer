using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using VueCliMiddleware;

namespace Summer.Core
{
    public static class SummerApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSummer(this IApplicationBuilder app)
        {
            var summerOptions = app.ApplicationServices.GetRequiredService<IOptions<SummerOptions>>().Value;

            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            if (!(summerOptions.DisableSwaggerInProd && env.IsProduction()))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Summer API V1");
                });
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (!env.IsDevelopment()) return;
                var launchMode = Environment.GetEnvironmentVariable("LAUNCH_MODE");
                switch (launchMode)
                {
                    case "Server":
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:9528");
                        break;
                    case "ServerClient":
                        spa.UseVueCli(npmScript: "serve", port: 9528); // optional port
                        break;
                }
            });

            return app;
        }
    }
}
