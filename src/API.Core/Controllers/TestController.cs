using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Core.Common.Helper;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

       
        private readonly IBinServices _binArticleServices;
        private readonly ILogger<TestController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="BindvertisementServices">注册bin服务</param>
        /// <param name="logger">注册日志</param>
        public TestController(IBinServices BindvertisementServices, ILogger<TestController> logger)
        {
            _logger = logger;
            _binArticleServices = BindvertisementServices;
        }


       
        [HttpGet] // GET: api/Test
        public async Task<object> GetAsync()
        {
           
            var model = await _binArticleServices.GetBinList();
            var data = new { success = true, data = model };
            return data;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<BinInfo>> GetAll()
        {
            var model = await _binArticleServices.Query();
            return model;
        }

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <returns></returns>
        // GET: api/Test
        [HttpGet]
        [Route("LeagueTableTest")]
        public async Task<ActionResult<IEnumerable<BinInfo>>> LeagueTableTest()
        {
            //调用该方法，这里 _blogArticleServices 是依赖注入的实例，不是类
            List<BinInfo> model = await _binArticleServices.TestGetBinList();
            var data = new { success = true, data = model };
            return model;
        }

        /// <summary>
        /// 动态表达式调取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("DynamicBehaviour")]
        public async Task<object> DynamicBehaviour()
        {
           
            var model = await _binArticleServices.DynamicBehaviour();
            var data = new { success = true, data = model };
            return data;
        }


        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="binInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddBin")]
        public async Task<MessageModel<string>> AddBin([FromBody] BinInfo binInfo) 
        {
            var data = new MessageModel<string>();
            binInfo.CreateTime= DateTime.Now;

            var id = (await _binArticleServices.Add(binInfo));
            data.Success = id > 0;
            
            if (data.Success)
            {
                data.Code = (int)CodeStatus.Success;
                data.Response = id.ObjToString();
                data.Msg = "添加成功";
            }

            return data;
        }

      




    }
}
