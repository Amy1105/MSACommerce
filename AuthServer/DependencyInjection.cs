using AuthServer.Clients;
using AuthServer.Services;
using CommonServiceClient;

namespace AuthServer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 配置UserServiceClient、IdentityService服务注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpApi(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureUserService(services, configuration);

            ConfigureIdentity(services, configuration);

            return services;
        }

        private static void ConfigureUserService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceClient<UserServiceClient>(option =>
            {
                option.LoadBalancingStrategy = LoadBalancingStrategy.RoundRobin;
            }, client =>
            {
                client.Timeout = TimeSpan.FromSeconds(1);
            });
        }

        private static void ConfigureIdentity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            // 从配置文件中读取JwtSettings，并注入到容器中
            var configurationSection = configuration.GetSection(nameof(JwtSettings));
            var jwtSettings = configurationSection.Get<JwtSettings>();
            if (jwtSettings is null) throw new NullReferenceException(nameof(jwtSettings));
            services.Configure<JwtSettings>(configurationSection);
        }
    }

}
