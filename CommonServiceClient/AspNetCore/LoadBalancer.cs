using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceClient.AspNetCore
{
    public class LoadBalancer<T>(LoadBalancingStrategy strategy)
     : LoadBalancer(strategy), ILoadBalancer<T> where T : class;

}
