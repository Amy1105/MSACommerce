using Common.HttpApi;

namespace UserService.HttpApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHttpApi(this IServiceCollection services)
        {
            services.AddHttpApiCommon();

            return services;
        }
    }
}
