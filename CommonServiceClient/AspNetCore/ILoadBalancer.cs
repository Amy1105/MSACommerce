using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceClient.AspNetCore
{
    public interface ILoadBalancer<T> : ILoadBalancer where T : class;

}
