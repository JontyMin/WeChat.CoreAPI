using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Core.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using WeChat.Core.Common.Helper;
using WeChat.Core.Common.Qiniu;
using WeChat.Core.IService;
using WeChat.Core.Model.ViewModel;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 图片上传
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : Controller
    {
        private readonly string Domain = AppSettings.app(new string[] { "AppSettings", "Qiniu", "Domain" });
        private readonly int MaxSize = AppSettings.app(new string[] {"AppSettings", "Qiniu", "MaxSize"}).ObjToInt();
        private readonly IWeChatSupplyProductImgService _weChatSupplyProductImgService;
        private readonly IHostEnvironment _environment;


        public ImageController(IHostEnvironment environment,
            IWeChatSupplyProductImgService weChatSupplyProductImgService)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _weChatSupplyProductImgService = weChatSupplyProductImgService ??
                                             throw new ArgumentNullException(nameof(weChatSupplyProductImgService));
        }

        /// <summary>
        /// 上传图片(七牛云)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Qiniu")]
        public async Task<MessageModel<List<ImageViewModel>>> Post(IFormFileCollection files)
        {
            var data = new MessageModel<List<ImageViewModel>>();

            //IFormFileCollection files = Request.Form.Files;

            if (files == null || !files.Any())
            {
                data.status = 400;
                data.msg = "请选择上传的文件。";
                return data;
            }

            //格式限制
            var allowType = new string[] {"image/jpg", "image/png", "image/jpeg", "image/gif"};

            if (!files.Any(x=>allowType.Contains(x.ContentType)))
            {
                data.status = 400;
                data.msg = "图片格式错误";
                return data;
            }
            //大小限制
            if (files.Sum(x => x.Length) >= MaxSize * 1024 * 1024)
            {
                data.status = 400;
                data.msg = "图片大小超出限制";
                return data;
            }

            QiniuSDK qiniu = new QiniuSDK();

            List<ImageViewModel> images = new List<ImageViewModel>();
            foreach (var file in files)
            {
                var imgUrl = await qiniu.UploadImageAsync(file);
                if (!string.IsNullOrEmpty(imgUrl))
                {
                    ImageViewModel image = new ImageViewModel()
                    {
                        ImageUrl =imgUrl
                    };
                    images.Add(image);
                    data.status = 200;
                    data.success = true;
                    data.msg = "上传成功";
                }
            }
            data.response = images;
            return data;
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="key">图片链接</param>
        /// <returns><see cref="MessageModel{T}"/></returns>
        [HttpDelete]
        //[AllowAnonymous]
        public async Task<MessageModel<string>> Delete(string key)
        {
            var data = new MessageModel<string>();

            if (!key.Contains(Domain))
            {
                data.msg = "key参数不合法";
                data.status = 400;
                return data;
            }
            QiniuSDK qiniu = new QiniuSDK();
            var state = await qiniu.DeleteImageSync(key);

            var imgs=await _weChatSupplyProductImgService.Query(x => x.ImageUrl == key);
            if (imgs.Any())
            {
                state = await _weChatSupplyProductImgService.DeleteById(imgs.FirstOrDefault().Id);
            }
            
            if (state)
            {
                data.success = true;
                data.status = 200;
                data.msg = "删除成功";
                return data;
            }

            return data;

        }

        /// <summary>
        /// 上传图片(本地)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Aliyun")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<MessageModel<List<ImageViewModel>>> Post()
        {
            var data = new MessageModel<List<ImageViewModel>>();
            string path = string.Empty;
            string foldername = "images";
            IFormFileCollection files = null;

            try
            {
                files = Request.Form.Files;
            }
            catch (Exception)
            {
                files = null;
            }

            if (files == null || !files.Any())
            {
                data.msg = "请选择上传的文件。";
                return data;
            }

            //格式限制
            var allowType = new string[] {"image/jpg", "image/png", "image/jpeg"};

            string folderpath = Path.Combine(_environment.ContentRootPath, foldername);
            if (!System.IO.Directory.Exists(folderpath))
            {
                System.IO.Directory.CreateDirectory(folderpath);
            }

            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    List<ImageViewModel> urls = new List<ImageViewModel>();
                    foreach (var file in files)
                    {
                        //var file = files.FirstOrDefault();
                        string strpath = Path.Combine(foldername, DateTime.Now.ToString("MMddHHmmss") + file.FileName);
                        path = Path.Combine(_environment.ContentRootPath, strpath);

                        using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            await file.CopyToAsync(stream);
                        }

                        ImageViewModel model = new ImageViewModel()
                        {
                            ImageUrl = path
                        };
                        urls.Add(model);
                    }

                    data = new MessageModel<List<ImageViewModel>>()
                    {
                        response = urls,
                        msg = "上传成功",
                        success = true,
                    };
                    return data;
                }

                data.msg = "图片过大";
                return data;
            }

            data.msg = "图片格式错误";
            return data;
        }
    }
}
