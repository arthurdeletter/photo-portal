﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoPortal.Heartcore;
using System.Configuration;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

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
            services.AddSingleton<UmbracoService>();

            var umbracoConfig = Configuration.GetSection("Heartcore");
            var projectAlias = umbracoConfig.GetValue<string>("ProjectAlias");
            var apiKey = umbracoConfig.GetValue<string>("ApiKey");

            services.AddUmbracoHeadlessContentDelivery(projectAlias, apiKey);
            services.AddUmbracoHeartcore(options =>
            {
                options.AddModels(typeof(Program).Assembly);
                options.ProjectAlias = projectAlias;
                options.ApiKey = apiKey;
            });
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