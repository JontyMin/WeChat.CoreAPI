using System;
using Newtonsoft.Json;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Nlp.V20190408;
using TencentCloud.Nlp.V20190408.Models;

namespace WeChat.Core.Common.WeChat.ChatRobot
{
    /// <summary>
    /// 腾讯闲聊机器人
    /// </summary>
    public class Robot
    {
        public string ChatBot(string query)
        {
            try
            {
                Credential cred = new Credential
                {
                    SecretId = "AKID3rNhofBSP6WIp5nyd3D5KL6bw5yb4lws",
                    SecretKey = "6yb7NW3OTq25sSBm9G2bx5xFJB4zf4MS"
                };

                ClientProfile clientProfile = new ClientProfile();
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("nlp.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                NlpClient client = new NlpClient(cred, "ap-guangzhou", clientProfile);
                ChatBotRequest req = new ChatBotRequest();
                req.Query = query;
                ChatBotResponse resp = client.ChatBotSync(req);
                Console.WriteLine(AbstractModel.ToJsonString(resp));

                return resp.Reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.Read();
            return "";
        }
    }

   
}