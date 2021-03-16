using System;

namespace WeChat.Core.Common.Helper
{
    public static class SignCheck
    {
        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="timespan"></param>
        /// <param name="nonce"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool ValidateSignature(string sign, string timespan, string nonce, string secretKey)
        {
            bool result = false;

            string[] arrTmp = {secretKey, timespan, nonce};
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = MD5Helper.MD5Encrypt(tmpStr, 32);
            Console.WriteLine(tmpStr);
            if (tmpStr == sign && IsNumberic(timespan))
            {
                DateTime date = UnixConvert.ToLocalTimeDateBySeconds(long.Parse(timespan));
                double minutes = DateTime.Now.Subtract(date).TotalMinutes;
                if (minutes < 5)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool IsNumberic(string message)
        {
            System.Text.RegularExpressions.Regex rex =
                new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(message)) return true;
            return false;
        }
    }
}