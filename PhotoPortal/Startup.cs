using PhotoPortal.Heartcore;
using MudBlazor.Services;
using Umbraco.Headless.Client.Net.Configuration;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PhotoPortal.Authentication;

namespace PhotoPortal
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddMudServices();

            services.AddHttpClient();

            // Auth
            services.AddScoped<ProtectedSessionStorage>();
            services.AddScoped<AuthenticationStateProvider, UmbracoAuthenticationStateProvider>();

            // Custom Umbraco Services
            services.AddSingleton<UmbracoService>();
            services.AddSingleton<UmbracoManagementService>();
            services.AddScoped<UmbracoAuthService>();

            var umbracoConfig = Configuration.GetSection("Heartcore");
            var projectAlias = umbracoConfig.GetValue<string>("ProjectAlias");
            var apiKey = umbracoConfig.GetValue<string>("ApiKey");

            services.AddUmbracoHeadlessContentDelivery(projectAlias, apiKey);
            services.AddUmbracoHeadlessContentManagement(new ApiKeyBasedConfiguration(projectAlias, apiKey));
            services.AddUmbracoHeadlessAuthentication(projectAlias, apiKey);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}