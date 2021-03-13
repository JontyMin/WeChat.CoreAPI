using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using WeChat.Core.Common.Helper;

namespace WeChat.Core.Common.Qiniu
{
    /// <summary>
    /// 七牛云SDK
    /// </summary>
    public class QiniuSDK
    {

        private readonly string AK = AppSettings.app(new string[] { "AppSettings", "Qiniu", "AccessKey" });
        private readonly string SK = AppSettings.app(new string[] { "AppSettings", "Qiniu", "SecretKey" });
        private readonly string Bucket = AppSettings.app(new string[] { "AppSettings", "Qiniu", "Bucket" });
        private readonly string Domain = AppSettings.app(new string[] { "AppSettings", "Qiniu", "Domain" });

        private Mac mac;
        public QiniuSDK()
        {
            mac = new Mac(AK, SK);
        }
        
        /// <summary>
        /// 上传图片(七牛云)
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            string imageUrl = string.Empty; 
            //Mac mac = new Mac(AK, SK);

            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = Bucket;
            putPolicy.SetExpires(7200);
            string jsonStr = putPolicy.ToJsonString();
            var token = Auth.CreateUploadToken(mac, jsonStr);

            Config config = new Config
            {
                Zone = Zone.ZoneCnSouth,
                UseHttps = true,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K
            };

            FormUploader upload = new FormUploader(config);
            
            var extension = Path.GetExtension(file.FileName)?.ToLower();
            Console.WriteLine(extension);
            
            Stream stream = file.OpenReadStream();
            var key = $"LZ{Guid.NewGuid()}{extension}";
            HttpResult result = await upload.UploadStream(stream, key, token, null);
            if (result.Code == (int)HttpCode.OK)
            {
                imageUrl = $"{Domain}{key}";
                return imageUrl;
            }

            return imageUrl;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> DeleteImageSync(string key)
        {
            // 截取除域名外的key
            key = key.Substring(key.LastIndexOf('/') + 1);

            // 设置存储区域
            Config config = new Config();
            config.Zone=Zone.ZoneCnSouth;

            //Mac mac = new Mac(AK, SK);
            BucketManager bucketManager = new BucketManager(mac, config);

            HttpResult result =await bucketManager.Delete(Bucket, key);
            if (result.Code==(int)HttpCode.OK)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<bool> BatchAsync(string[] keys)
        {
            // 设置存储区域
            Config config = new Config();
            config.Zone = Zone.ZoneCnSouth;
            //Mac mac = new Mac(AK, SK);
            BucketManager bucketManager = new BucketManager(mac, config);

            List<string> ops = new List<string>();
            foreach (var key in keys)
            {
                string op = bucketManager.DeleteOp(Bucket, key);
                ops.Add(op);
            }

            BatchResult result =await bucketManager.Batch(ops);
            if (result.Code/100==2)
            {
                return true;
            }

            return false;
        }
    }
}