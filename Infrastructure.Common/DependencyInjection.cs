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
            //配置服务发现中心功能。包括指定Consul中心地址、健康检查
            services.AddConsulService(serviceConfiguration =>
            {
                serviceConfiguration.ServiceAddress = new Uri(configuration["urls"] ?? configuration["applicationUrl"]);
            }, serviceCheck);

            //配置服务发现功能
            services.AddConsulDiscovery();
        }
    }

}
