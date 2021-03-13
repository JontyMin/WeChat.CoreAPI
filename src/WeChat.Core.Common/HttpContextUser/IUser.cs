using System.Collections.Generic;
using System.Security.Claims;

namespace WeChat.Core.Common.HttpContextUser
{
    public interface IUser
    {
        /// <summary>
        /// 请求用户Id
        /// </summary>
        string Uid { get; }
        
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        List<string> GetClaimValueByType(string ClaimType);
    }
}