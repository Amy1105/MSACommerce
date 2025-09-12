using Consul;
using Consul.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Consul.ServiceRegistration
{
    public static class ConsulServiceRegistrationExtensions
    {
        public static IServiceCollection AddConsulService(this IServiceCollection services,
            Action<ServiceConfiguration> serviceConfigure,
            ServiceCheckConfiguration? serviceCheckConfiguration)
        {
            var serviceConfiguration = new ServiceConfiguration();
            serviceConfigure.Invoke(serviceConfiguration);

            serviceCheckConfiguration ??= new ServiceCheckConfiguration();

            var healthCheckUri = new UriBuilder(serviceConfiguration.ServiceAddress)
            {
                Path = serviceCheckConfiguration.Path
            };

            services.AddConsulServiceRegistration(serviceRegistration =>
            {
                serviceRegistration.ID = serviceConfiguration.ServiceId;
                serviceRegistration.Name = serviceConfiguration.ServiceName;
                serviceRegistration.Address = serviceConfiguration.ServiceAddress.Host;
                serviceRegistration.Port = serviceConfiguration.ServiceAddress.Port;
                serviceRegistration.Check = new AgentServiceCheck
                {
                    HTTP = healthCheckUri.ToString(),
                    Interval = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Interval),
                    Timeout = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Timeout),
                    DeregisterCriticalServiceAfter = TimeSpan.FromMilliseconds(serviceCheckConfiguration.Deregister),
                };
            });

            return services;
        }
    }

}
