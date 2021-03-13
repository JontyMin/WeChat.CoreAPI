﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using WeChat.Core.Common.Helper;

namespace WeChat.Core.Common.HttpContextUser
{
    public class AspNetUser:IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Uid => GetName();

        private string GetName()
        {
            if (IsAuthenticated())
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                {
                    return GetUserInfoFromToken("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").FirstOrDefault().ObjToString();
                }
            }
            return "";
        }

        public string GetToken()
        {
            return _accessor.HttpContext.Request.Headers["Authorization"].ObjToString().Replace("Bearer", "");
        }
        
        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public List<string> GetUserInfoFromToken(string claimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            if (!string.IsNullOrEmpty(GetToken()))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(GetToken());

                return (from item in jwtToken.Claims
                    where item.Type == claimType
                    select item.Value).ToList();
            }
            else
            {
                return new List<string>() { };
            }
        }


        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                where item.Type == ClaimType
                select item.Value).ToList();
        }
    }
}