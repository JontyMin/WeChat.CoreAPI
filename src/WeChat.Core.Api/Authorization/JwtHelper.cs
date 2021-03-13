using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WeChat.Core.Common.Helper;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WeChat.Core.Api.Authorization
{
    public class JwtHelper
    {
        /// <summary>
        /// 获取token信息=>加密
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static TokenInfoViewModel IssueJwt(TokenModel tokenModel)
        {
            //获取Appsettings配置信息
            string iss = AppSettings.app(new string[] { "AppSettings", "JwtSetting", "Issuer" });
            string aud = AppSettings.app(new string[] { "AppSettings", "JwtSetting", "Audience" });
            string secret = AppSettings.app(new string[] { "AppSettings", "JwtSetting", "SecretKey" });
            int expire = AppSettings.app(new string[] {"AppSettings", "JwtSetting", "Expiration"}).ObjToInt();
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,tokenModel.sId.ToString()),
                
                new Claim(JwtRegisteredClaimNames.Jti,tokenModel.sId.ToString()),
                // 签发时间
                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                //
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                // 过期时间 2h
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(expire)).ToUnixTimeSeconds()}"),
                
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(expire).ToString(CultureInfo.InvariantCulture)),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud)
            };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new TokenInfoViewModel()
            {
                success = true,
                token = encodedJwt,
                expires_in = expire,
                token_type = "Bearer"
            };
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            var tm = new TokenModel
            {
                sId = Convert.ToInt32(jwtToken.Id),
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }

    }

}