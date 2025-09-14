using Infrastructure.Common;
using Infrastructure.EntityFrameworkCore;
using Infrastructure.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 配置基础设施
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //配置基础服务发现
            services.AddInfrastructureCommon(configuration);

            //配置efcore过滤器
            services.AddInfrastructureEfCore();

            //配置AddDbContext
            ConfigureEfCore(services, configuration);

            return services;
        }
     
        private static void ConfigureEfCore(IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("UserDbConnection");

            services.AddDbContext<UserDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<AuditEntityInterceptor>());
                options.UseMySql(dbConnection, ServerVersion.AutoDetect(dbConnection));
            });
        }
    }

}
