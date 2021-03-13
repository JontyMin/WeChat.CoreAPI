using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using WeChat.Core.Common.WeChat.ChatRobot;
using static System.TimeZone;

namespace WeChat.Core.Common.WeChat.Reply
{
    public static class ReplayMessage
    {
        /// <summary>
         /// 处理微信发过来的消息
         /// </summary>
         /// <param name="xml"></param>
         /// <returns></returns>
         public static string ParseMessage(string xml)
         {
             if (string.IsNullOrEmpty(xml))
             {
                 return "success";
             }
             var dict = Xml2Dict(xml);
             string result = string.Empty;
             switch (dict["MsgType"].ToLower())
             {
                 //文本类型
                 case "text":
                     TextMessageHandler(dict, ref result);
                     break;

                 //事件类型
                 case "event":
                     string openid = dict["FromUserName"];
                     switch (dict["Event"].ToLower())
                     {

                        //用户关注事件
                        case "subscribe":
                            SubscribeEvent(dict, ref result);
                            break;
                        case "click":
                            switch (dict["EventKey"].ToLower())
                            {
                                case "lzcompanyculture": // 企业文化
                                    NewsMessageHandler(dict, ref result);
                                    break;
                                case "lzrecruitment": // 企业招聘
                                    NewsMessageHandler(dict, ref result);
                                    break;
                                case "goodsalesnews": // 销售喜报
                                    break;
                                case "salesnews": // 销售动态
                                    break;
                                case "lzordernews": // 订单动态
                                    break;

                            }
                            break;
                     }

                    break;
                 default:
                     result="服务器君走丢了~";
                     break;
             }

             return result;
        }



        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="result"></param>
        private static void TextMessageHandler(Dictionary<string, string> dict, ref string result)
        {
            
            ReplayTextMessage message = new ReplayTextMessage(dict["Content"])
            {
                ToUserName = dict["FromUserName"],
                FromUserName = dict["ToUserName"],
                CreateTime = Timestamp().ToString()
            };
            result = message.ToXml();
        }

        private static void NewsMessageHandler(Dictionary<string, string> dict, ref string result)
        {
            ReplayNewsMessage message = new ReplayNewsMessage()
            {
                ToUserName = dict["FromUserName"],
                FromUserName = dict["ToUserName"],
                CreateTime = Timestamp().ToString(),
                Count = 1,
                Title = "企业文化",
                Description = "联洲电子商务有限公司简介",
                PicUrl = "https://cdn.jonty.top/LZ6adfca13-de3e-4268-a532-74de9c407592.jpg",
                Url = "https://mp.weixin.qq.com/s/308uAUD0y_KSwrmhc1z1YQ"
            };
            result = message.ToXml();
        }

        /// <summary>
        /// 当用户关注公众号时，进行的操作
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static void SubscribeEvent(Dictionary<string, string> dict, ref string result)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("您好,欢迎关注联洲跨境!请及时绑定供应商账号,绑定后订单通知将第一时间通知您。");
            builder.AppendLine("合作供应商点击进入登录页面,输入您的供应商账号及密码,即可绑定成功!<a href='https://wx.lianzhoukuajing.com'>去绑定</a>");
            builder.AppendLine(
                "未合作供应商点击进入供应商入驻界面,申请入驻。供应商入驻审核通过后，将短信发送快速邀请码。<a href='https://wx.lianzhoukuajing.com/#/apply'>去入驻</a>");
            builder.AppendLine("获得邀请码的供应商可点击下方快速注册入驻联洲。<a href='https://wx.lianzhoukuajing.com'>去注册</a>");

            
            string openid = dict["FromUserName"];
            //关注时发送的消息
            ReplayTextMessage message = new ReplayTextMessage()
            {
                ToUserName = dict["FromUserName"],
                FromUserName = dict["ToUserName"],
                CreateTime = Timestamp().ToString(),
                Content = builder.ToString()
                    
            };
            result = message.ToXml();
        }


        #region PrivateMethod

        /// <summary>
        /// xml转dic
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static Dictionary<string, string> Xml2Dict(string xml)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                result.Add(node.Name, node.InnerText);
            }

            return result;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <returns>Unix时间戳格式  单位：毫秒</returns>
        private static long Timestamp()
        {
            DateTime startTime = CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(DateTime.Now - startTime).TotalSeconds * 1000;
        }

        #endregion

    }
}