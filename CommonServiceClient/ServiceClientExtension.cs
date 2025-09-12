using Common.Consul.ServiceDiscovery;
using CommonServiceClient.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
namespace CommonServiceClient
{
    public static class ServiceClientExtension
    {
        public static void AddServiceClient<TServiceClient>(this IServiceCollection services,
            Action<ServiceClientOption> configureServiceClient,
            Action<HttpClient> configureHttpClient)
            where TServiceClient : class, ISeviceClient
        {
            var serviceClientOption = new ServiceClientOption();
            configureServiceClient.Invoke(serviceClientOption);

            services.AddConsulDiscovery();

            services.AddLoadBalancer<TServiceClient>(serviceClientOption.LoadBalancingStrategy);

            services.AddHttpClient<TServiceClient>(configureHttpClient);

            services.AddScoped<TServiceClient>();
        }
    }

}
