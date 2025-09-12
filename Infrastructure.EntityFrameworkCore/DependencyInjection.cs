using Infrastructure.EntityFrameworkCore.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFrameworkCore
{
    /// <summary>
    /// 注册拦截器中间件
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureEfCore(this IServiceCollection services)
        {
            services.AddScoped<AuditEntityInterceptor>();

            return services;
        }
    }
}
