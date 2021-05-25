using FC_NDIS.Action;
using FC_NDIS.ActionInterface;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC_NDIS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var section = Configuration.GetSection(nameof(IntegrationAppSettings));
            var integrationAppSettings = section.Get<IntegrationAppSettings>();
            services.AddSingleton(integrationAppSettings);            
            services.AddScoped<ISFDC, SFDCRestAPIAccess>();
            services.AddScoped<IConnex, ConnexServiceAction>();
            services.AddScoped<IFleetComplete, FleetCompleteAction>();
            services.AddSwaggerGen();
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());     
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        //  IServiceProvider serviceProvider)
        {
            var section = Configuration.GetSection(nameof(IntegrationAppSettings));
            var integrationAppSettings = section.Get<IntegrationAppSettings>();


            if (env.IsDevelopment() || env.IsProduction()||env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseHangfireDashboard();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
