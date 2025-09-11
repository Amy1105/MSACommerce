using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddUseCaseCommon(this IServiceCollection services, Assembly assembly)
        {            
            //services.AddAutoMapper(typeof(assembly), typeof(ProfileTypeFromAssembly2));

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
