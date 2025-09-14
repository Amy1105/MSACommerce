using AuthServer.Clients;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.Result;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Services
{
    public class IdentityService(UserServiceClient userServiceClient, IOptions<JwtSettings> jwtSettings) : IIdentityService
    {
        /// <summary>
        /// 用户登录获取token信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>访问令牌</returns>
        public async Task<Result<string>> GetAccessTokenAsync(string username, string password)
        {
            // 验证用户名和密码
            var response = await userServiceClient.UserServiceApi.GetUserAsync(username, password);
            if (!response.IsSuccessStatusCode)
            {
                return Result.Failure("用户名或密码不正确");
            }
            var user = response.Content;

            // 创建 JWT
            var jwt = new JwtSecurityToken(
                jwtSettings.Value.Issuer,
                jwtSettings.Value.Audience,
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.MobilePhone, user.Phone ?? string.Empty)
                },
                expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret!)),
                    SecurityAlgorithms.HmacSha256)
            );

            // 生成 JWT 字符串
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token is null ? Result.Failure() : Result.Success(token);
        }
    }
}
