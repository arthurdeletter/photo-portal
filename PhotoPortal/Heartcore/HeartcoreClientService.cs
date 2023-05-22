using System;
using System.Net;
using Umbraco.Headless.Client.Net.Configuration;
using Umbraco.Headless.Client.Net.Delivery;
using Umbraco.Headless.Client.Net.Management;
using Umbraco.Headless.Client.Net.Security;

namespace PhotoPortal.Heartcore
{
    // Github reference: https://github.com/umbraco/Umbraco.Headless.Client.Net/blob/master/samples/Umbraco.Headless.Client.Samples.BlazorServer/Umbraco.Headless.Client.Samples.BlazorServer/Heartcore/HeartcoreClientService.cs

    public static class HeartcoreClientService
    {
        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
        string projectAlias, string apiKey = null)
        {
            services.AddSingleton
                (string.IsNullOrEmpty(apiKey)
                ? new ContentDeliveryService(projectAlias)
                : new ContentDeliveryService(projectAlias, apiKey));
            services.AddUmbracoHeartcore(options =>
            {
                options.AddModels(typeof(Program).Assembly);
            });
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
             IHeadlessConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentDelivery(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentDeliveryService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IApiKeyBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentManagementService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessContentManagement(this IServiceCollection services,
            IPasswordBasedConfiguration configuration)
        {
            services.AddSingleton(new ContentManagementService(configuration));
            return services;
        }

        public static IServiceCollection AddUmbracoHeadlessAuthentication(this IServiceCollection services, string projectAlias, string apiKey = null)
        {

            HttpClient client = new()
            {
                BaseAddress = new Uri("https://cdn.umbraco.io/"),
            };
            client.DefaultRequestHeaders.Add("api-key", apiKey);

            services.AddSingleton(new AuthenticationService(projectAlias, client));
            return services;
        }
    }
}

