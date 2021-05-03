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
            services.AddScoped<ISFDC, SFDCAction>();
            services.AddScoped<IConnex, ConnexServiceAction>();
            services.AddScoped<IFleetComplete, FleetCompleteAction>();
            services.AddSwaggerGen();
            services.Configure<IISServerOptions>(options => { options.AllowSynchronousIO = true; });
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());
            //.UseSqlServerStorage(Configuration.GetConnectionString("sqlConnection")));


            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            var section = Configuration.GetSection(nameof(IntegrationAppSettings));
            var integrationAppSettings = section.Get<IntegrationAppSettings>();

            string userlist;
            List<string> UserNames = new List<string>();
            SFDCAction sfdca = new SFDCAction(integrationAppSettings);
            UserNames = sfdca.GetAllDriverInfo_NotMappedSFDC();
            userlist = "'" + string.Join("','", UserNames.Where(k => !string.IsNullOrEmpty(k))) + "'";

            FleetCompleteAction fca = new FleetCompleteAction(integrationAppSettings);
            var url = "https://hosted.fleetcomplete.com.au/Authentication/v9/Authentication.svc/authenticate/user?clientId=" + 46135 + "&userLogin=" + integrationAppSettings.UserName + "&userPassword=" + integrationAppSettings.Password;
            var tokeninfo = fca.GetAccessToken(url);

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

            recurringJobManager.AddOrUpdate(
                "Scheduled Customer Service Line",
                () => serviceProvider.GetService<ISFDC>().IntegerateSfCustServiceLine(integrationAppSettings.SFDCUserName,integrationAppSettings.SFDCUserPassword),
                integrationAppSettings.CSLineScheduleTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                );
            recurringJobManager.AddOrUpdate(
                "Scheduled Customer List",
                () => serviceProvider.GetService<ISFDC>().IntegerateSfCustomeList(integrationAppSettings.SFDCUserName, integrationAppSettings.SFDCUserPassword),
                integrationAppSettings.CustomerListScheduleTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                );
            recurringJobManager.AddOrUpdate(
                "Scheduled Driver",
                () => serviceProvider.GetService<ISFDC>().IntegrateSFDCId_OperatortoDB(userlist, integrationAppSettings.SFDCUserName, integrationAppSettings.SFDCUserPassword),
                integrationAppSettings.DriverScheduleTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                );
            recurringJobManager.AddOrUpdate(
                "Scheduled Fleet complete asset",
                () => serviceProvider.GetService<IFleetComplete>().IntegrateAsset(integrationAppSettings.ClientID, tokeninfo.UserId, tokeninfo.Token),
                integrationAppSettings.FCAssetScheduleTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                );
            recurringJobManager.AddOrUpdate(
                "Scheduled Connx Driver",
                () => serviceProvider.GetService<IConnex>().IntegrateDriverDetails(integrationAppSettings.ConnexUserName, integrationAppSettings.ConnexUserPassword),
                integrationAppSettings.ConnxScheduleTime, TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time")
                );
        }
    }
}
