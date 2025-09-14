using AuthServer.Services;
using Common.Consul.ServiceDiscovery;
using CommonServiceClient;
using CommonServiceClient.AspNetCore;
using Refit;

namespace AuthServer.Clients
{
    /// <summary>
    /// UserServiceClient构造函数，定义服务名称，发现服务
    /// </summary>
    /// <param name="serviceDiscovery">consul服务发现</param>
    /// <param name="loadBalancer">负载均衡策略</param>
    /// <param name="httpClient"></param>
    public class UserServiceClient(
      IServiceDiscovery serviceDiscovery,
      ILoadBalancer<UserServiceClient> loadBalancer,
      HttpClient httpClient)
      : ServiceClient(serviceDiscovery, loadBalancer, httpClient)
    {
        public override string ServiceName { get; set; } = "Zhaoxi.MSACommerce.UserService.HttpApi";

        public readonly IUserService UserServiceApi = RestService.For<IUserService>(httpClient);
    }

}
