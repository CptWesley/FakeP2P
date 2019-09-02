using System.Diagnostics.CodeAnalysis;
using FakeP2P.Hubs;
using FakeP2P.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FakeP2P
{
    /// <summary>
    /// The startup class used.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1052", Justification = "Can't be static.")]
    public class Startup
    {
        /// <summary>
        /// Adds service containers.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSingleton<ServerService>();
            services.AddSwaggerDocument();
        }

        /// <summary>
        /// Configures HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The environment.</param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ServerHub>("/signalr/servers");
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
