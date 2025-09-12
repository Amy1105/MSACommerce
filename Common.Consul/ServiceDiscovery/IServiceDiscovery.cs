using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Consul.ServiceDiscovery
{
    public interface IServiceDiscovery
    {
        Task<List<string>> GetServicesAsync(string serviceName);
    }
}
