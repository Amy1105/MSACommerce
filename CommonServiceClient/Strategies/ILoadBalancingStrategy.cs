using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceClient.Strategies
{
    public interface ILoadBalancingStrategy
    {
        string Resolve(List<string> nodes);
    }
}
