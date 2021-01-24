using System;
using Akka.Actor;
using AkkaHost.Applications.Services;
using AkkaHost.Domain.Actors;
using AkkaHost.Domain.Repositories;
using AkkaHost.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AkkaHost
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IClientServices>(sp => new ClientServices(ActorSystemManager.ClientActor));
            services.AddSingleton<ClientActorStore>();
            services.AddSingleton<IClientRepository,ClientDao>();
            services.AddSingleton<Func<IClientRepository>>(sp => () => sp.GetService<IClientRepository>());

            services.AddHostedService<ActorSystemManager>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
