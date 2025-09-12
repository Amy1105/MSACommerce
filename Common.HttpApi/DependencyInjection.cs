using Common.HttpApi.Infrastructure;
using Common.HttpApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UseCases.Common.Interfaces;

namespace Common.HttpApi
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 这个跟AppBuilderExtensions使用场景有什么不同？ to do。。。
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpApiCommon(this IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddScoped<IUser, CurrentUser>();

            services.AddHttpContextAccessor();

            services.AddExceptionHandler<UseCaseExceptionHandler>();

            services.AddProblemDetails();

            ConfigureCors(services);

            ConfigureSwagger(services);

            return services;
        }

        private static void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAny", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }


        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "电商平台 API 文档",
                    Version = "v1",
                    Description = "一个微服务架构的电商平台实战项目"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "JWT Bearer 认证",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new string[] { }
                }
            });
            });
        }
    }
}