using Common.Consul.ServiceRegistration;
using Common.Consul.ServiceDiscovery ;
using Consul.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureCommon(this IServiceCollection services,
            IConfiguration configuration)
        {
            ConfigureConsul(services, configuration);

            return services;
        }

        private static void ConfigureConsul(IServiceCollection services, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection("ServiceCheck");
            var serviceCheck = configurationSection.Get<ServiceCheckConfiguration>();
            services.Configure<ServiceConfiguration>(configurationSection);

            services.AddConsul();
            services.AddConsulService(serviceConfiguration =>
            {
                serviceConfiguration.ServiceAddress = new Uri(configuration["urls"] ?? configuration["applicationUrl"]);
            }, serviceCheck);

            services.AddConsulDiscovery();
        }
    }

}
