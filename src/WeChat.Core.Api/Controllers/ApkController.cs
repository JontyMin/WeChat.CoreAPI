using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Crypto.Engines;
using WeChat.Core.Common.Helper;
using WeChat.Core.Model;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// apk 安装包控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "UniApp")]
    public class ApkController : ControllerBase
    {

        private readonly string _version = AppSettings.app(new string[] {"AppSettings", "Apk", "Version"});
        private readonly string _apkPath = AppSettings.app(new string[] {"AppSettings", "Apk", "ApkFilePath" });
        private readonly IHostEnvironment _hostEnvironment;


        public ApkController(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// 上传apk包
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<MessageModel<string>> Post(IFormFile file)
        {
            var data = new MessageModel<string>();
            if (file == null)
            {
                data.status = 400;
                data.msg = "请选择上传的文件";
                return data;
            }

            if (Path.GetExtension(file.FileName) != ".apk")
            {
                data.status = 400;
                data.msg = "文件格式错误";
                return data;
            }

            string webRootPath = _hostEnvironment.ContentRootPath;
            
            string fileName = "Depot.apk";
            string filePath = webRootPath + _apkPath;
            DirectoryInfo directory = new DirectoryInfo(filePath);

            if (!directory.Exists)
            {
                directory.Create();
            }

            await using (var stream = System.IO.File.Create(filePath + fileName))
            {
                await file.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            data.status = 200;
            data.success = true;
            data.msg = "发包完成";
            return data;
        }


        /// <summary>
        /// 版本校验
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("{version}")]
        public IActionResult Get(string version)
        {
            if (version == _version)
            {
                return Ok(new {result = false});
            }

            return Ok(new {result = true});
        }
        
        /// <summary>
        /// 安装包下载
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("download/Depot.apk")]
        public async Task<IActionResult> Download()
        {
            string webRootPath = _hostEnvironment.ContentRootPath;
            string filePath = webRootPath + _apkPath + "Depot.apk";
            await using var fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, bytes.Length);
            return File(bytes, "application/vnd.android.package-archive");
        }
    }
}
