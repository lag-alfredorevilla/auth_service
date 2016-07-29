using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Metrics.Asp.Mvc.Extensions;
using System.Metrics.StatsD;
using AuthService.Repositories;
using System;

namespace AuthService.Hosting
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup (IHostingEnvironment env)
        {
            _env = env;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddLeanMvc();

            services.AddSingleton<IAuthProviderRepository>(x => new FileAuthProviderRepository(_env));
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);

            // Adds the metrics.
            services.AddMetrics(settings => { settings.CountHitsTotal();}, svc => { svc.AddStatsD(ep => ep.UseUdp());});
        }

        
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if(env.IsDevelopment())
            {
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }

            app.UseMvc();
        }


    }
}