using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CTFPlatForm.Infrastructure.Tools
{
    /// <summary>
    /// JWT认证辅助类
    /// </summary>
    public class JWTHelper
    {

        #region 构造函数
        protected readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        /// <summary>
        /// 生成 Json Web Token
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="account">用户账号</param>
        /// <returns></returns>
        public string GenerateJwtToken(string userId, string account)
        {
            // 获取配置参数
            var issuer = _configuration["JWTSettings:ValidIssuer"];
            var audience = _configuration["JWTSettings:ValidAudience"];
            var secretKey = _configuration["JWTSettings:IssuerSigningKey"];
            var expiredTime = int.Parse(_configuration["JWTSettings:ExpiredTime"]);
            var clockSkew = int.Parse(_configuration["JWTSettings:ClockSkew"]);

            // 创建签名密钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, _configuration["JWTSettings:Algorithm"]);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),//令牌主题，用户唯一标识符
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // 创建Token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiredTime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
