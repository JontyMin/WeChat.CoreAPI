using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeChat.Core.Api.Log;
using WeChat.Core.IService;
using WeChat.Core.Model;
using WeChat.Core.Model.EntityModel;
using WeChat.Core.Service;

namespace WeChat.Core.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly ILoggerHelper _logger;
        private readonly IWeChatSupplierService _weChatSupplierService;

        public HomeController(ITestService testService, ILoggerHelper loggerHelper,IWeChatSupplierService weChatSupplierService)
        {
            _weChatSupplierService = weChatSupplierService;
            _testService = testService;
            _logger = loggerHelper;
        }

        /// <summary>
        /// GetTest
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<WeChatSupplier>>> Get()
        {
            var data = new MessageModel<List<WeChatSupplier>>();
            data.msg = "OK";
            data.status = 200;
            data.success = true;
            data.response = await _weChatSupplierService.Queryable("select * from WeChatSupplier");
            return data;
        }
        
        /// <summary>
        /// 测试API方法注释
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public int SumService(int i, int j)
        {
            return _testService.SumService(i, j);
        }
        /// <summary>
        /// 测试日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("logtest")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult LogTest()
        {
            _logger.Error(typeof(HomeController), "这是错误日志", new Exception("123"));
            _logger.Debug(typeof(HomeController), "这个是bug日志");
            //throw new IOException();
            return Ok();
        }
        
    }
}
