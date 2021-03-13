using System;
using System.Linq;
using System.Text;

namespace WeChat.Core.Common.Helper
{
    /// <summary>
    /// 邀请码生成类
    /// </summary>
    public class CodeHelper
    {
        /// <summary>
        /// 自定义进制(排除0、O)
        /// </summary>
        private static char[] r = new char[] { 'Q', 'W', 'E', '8', 'A', 'S', '2', 'D', 'Z', '9', 'C', '7', 'P', '5', 'I', 'K', '3', 'M', 'J', 'U', 'F', 'R', '4', 'V', 'Y', 'L', 'T', 'N', '6', 'B', 'G', 'H' };
        /// <summary>
        /// X补位
        /// </summary>
        private static char b = 'X';
        /// <summary>
        /// 进制长度
        /// </summary>
        private static int binLen = r.Length;
        /// <summary>
        /// 邀请码长度
        /// </summary>
        private static int s = 6;

        /// <summary>
        /// 根据Id生成6位随机邀请码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Encode(int id)
        {
            char[] buf = new char[32];
            int charPos = 32;

            while ((id/binLen)>0)
            {
                int ind = (int) (id % binLen);
                buf[--charPos] = r[ind];
                id /= binLen;
            }

            buf[--charPos] = r[(int)(id % binLen)];
            String str = new String(buf, charPos, (32 - charPos));
            //不够长度的自动随机补全
            if (str.Length < s)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(b);
                Random rnd = new Random();
                for (int i = 1; i < s - str.Length; i++)
                {
                    sb.Append(r[rnd.Next(binLen)]);
                }
                str += sb.ToString();
            }
            return str;
        }

        /// <summary>
        /// 根据随机邀请码获取用户Id
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Decode(string code)
        {
            char[] chs = code.ToArray();
            long res = 0L;
            for (int i = 0; i < chs.Length; i++)
            {
                int ind = 0;
                for (int j = 0; j < binLen; j++)
                {
                    if (chs[i] == r[j])
                    {
                        ind = j;
                        break;
                    }
                }
                if (chs[i] == b)
                {
                    break;
                }
                if (i > 0)
                {
                    res = res * binLen + ind;
                }
                else
                {
                    res = ind;
                }
            }
            return res.ToString();
        }
    }
}