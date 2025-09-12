using Common.Consul.ServiceRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.HttpApi
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// 公共配置：1.配置健康检查；2.配置授权鉴权；3.配置异常中间件；
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHttpCommon(this IApplicationBuilder app)
        {
            var serviceCheck = app.ApplicationServices.GetRequiredService<IOptions<ServiceCheckConfiguration>>().Value;
            app.UseHealthChecks(serviceCheck.Path);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseExceptionHandler(_ => { });

            return app;
        }
    }
}
