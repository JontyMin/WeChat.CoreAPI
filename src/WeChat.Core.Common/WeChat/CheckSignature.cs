using System;
using System.Security.Cryptography;
using System.Text;
using WeChat.Core.Common.Helper;

namespace WeChat.Core.Common.WeChat
{
    /// <summary>
    /// 微信服务器验证
    /// </summary>
    public  class CheckSignature
    {
        /// <summary>
        /// 微信服务器配置token
        /// </summary>
        private  readonly string Token = AppSettings.app(new string[] {"AppSettings", "WeChat", "Token" });

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public  bool Check(string signature, string timestamp, string nonce)
        {
            string[] arr = {Token, timestamp, nonce};
            Array.Sort(arr);
            string tmpStr = string.Join("", arr);
            tmpStr = GetSHA1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
                return true;
            return false;
        }

        /// <summary>
        /// 获取加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetSHA1(string str)
        {
            SHA1 algorithm = SHA1.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }
    }
}