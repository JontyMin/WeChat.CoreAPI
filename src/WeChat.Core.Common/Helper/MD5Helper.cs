using System;
using System.Security.Cryptography;
using System.Text;

namespace WeChat.Core.Common.Helper
{
    public class MD5Helper
    {
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", string.Empty);
            return t2;
        }

        public static string MD5Entry(string str)
        {
            StringBuilder pwdstr = new StringBuilder("L#g^p");
            string s = pwdstr.Append(str).ToString();
            MD5 md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", null);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password = "")
        {
            string pwd = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(password) && !string.IsNullOrWhiteSpace(password))
                {
                    MD5 md5 = MD5.Create(); //实例化一个md5对像
                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
                    foreach (var item in s)
                    {
                        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                        pwd = string.Concat(pwd, item.ToString("X"));
                    }
                }
            }
            catch
            {
                throw new Exception($"错误的 password 字符串:【{password}】");
            }
            return pwd;
        }


        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="encoder">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回加密后的字符串</returns>
        public static string MD5Encrypt(string encoder, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(encoder));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            tmp.Replace(tmp.ToString(), tmp.ToString().ToUpper());//ToUpper将字符串转换为大写
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else if (bit == 32)
                return tmp.ToString(); //默认情况
            else
                return string.Empty;
        }

        /// <summary>
        /// 64位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            // 实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(s);
        }
    }
}