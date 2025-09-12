using Common.Consul.ServiceDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceClient
{
    public abstract class ServiceClient : ISeviceClient
    {
        public virtual string ServiceName { get; set; }

        protected ServiceClient(IServiceDiscovery serviceDiscovery,
            ILoadBalancer loadBalancer,
            HttpClient httpClient)
        {
            var serviceList = serviceDiscovery.GetServicesAsync(ServiceName).Result;
            var serviceAddress = loadBalancer.GetNode(serviceList);

            httpClient.BaseAddress = new Uri($"http://{serviceAddress}");
        }


    }
}
