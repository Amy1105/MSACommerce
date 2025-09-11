using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using UseCases.Common.Behaviors;

namespace UseCases.Common
{
    /// <summary>
    /// 安装包：
    /// FluentValidation.DependencyInjectionExtensions
    /// AutoMapper
    /// MediatR
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddUseCaseCommon(this IServiceCollection services, Assembly assembly, params Type[] types)
        {
            // 方式一. 创建配置表达式（这和以前的 cfg 参数是一样的）
            var configExpression = new MapperConfigurationExpression();
            configExpression.AddMaps(assembly); // 使用 AddMaps 扫描整个程序集，替代多个 AddProfile

            // 方式二. 使用 MapperConfiguration 来创建最终的配置
            // 你需要从 DI 容器中获取一个 ILoggerFactory 来创建 Logger
            var serviceProvider = services.BuildServiceProvider(); // 注意：这是一个临时 ServiceProvider
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
           // var logger = loggerFactory.CreateLogger<MapperConfiguration>();

            // 显式创建 MapperConfiguration
            var config = new MapperConfiguration(cfg =>
            {
                // 添加来自传入程序集的的所有 Profile
                cfg.AddMaps(assembly); // 这是关键！AddMaps 方法用于扫描程序集
                                       // 你也可以在这里手动添加其他配置，例如自定义类型转换器等
                                       // cfg.CreateMap<Source, Destination>();
                                       // cfg.CreateMap<string, int>().ConvertUsing(s => Convert.ToInt32(s)); // 例如全局类型转换器:cite[1]:cite[2]
            }, loggerFactory);


            // 3. 注册服务（保持不变）
            services.AddSingleton<IConfigurationProvider>(config);
            services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));


            //注册其他服务
            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            return services;
        }
    }
}
