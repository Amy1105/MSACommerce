using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServiceClient.AspNetCore
{
    public static class LoadBalancerExtensions
    {
        public static IServiceCollection AddLoadBalancer<T>(
            this IServiceCollection services,
            LoadBalancingStrategy strategy) where T : class
        {
            services.AddSingleton<ILoadBalancer<T>>(new LoadBalancer<T>(strategy));
            return services;
        }
    }

}
