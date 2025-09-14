using SharedKernel.Result;

namespace AuthServer.Services
{
    public interface IIdentityService
    {
        /// <summary>
        /// 用户登录获取token信息
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Result<string>> GetAccessTokenAsync(string username, string password);
    }

}
