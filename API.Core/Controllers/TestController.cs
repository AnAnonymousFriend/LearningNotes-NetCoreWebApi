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
        public TestController(IBinServices BindvertisementServices)
        {
           
            this._binArticleServices = BindvertisementServices;
        }


        // GET: api/Test
        [HttpGet]
        public async Task<object> GetAsync()
        {
           
            var model = await _binArticleServices.GetBinList();
            var data = new { success = true, data = model };
            return data;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<BinInfo>> GetAll()
        {
            var model = await _binArticleServices.Query();
            return model;
        }


        // GET: api/Test
        [HttpGet]
        [Route("LeagueTableTest")]
        public async Task<object> LeagueTableTest()
        {
            //调用该方法，这里 _blogArticleServices 是依赖注入的实例，不是类
            var model = await _binArticleServices.TestGetBinList();
            var data = new { success = true, data = model };
            return data;
        }

        // GET: api/Test
        [HttpGet]
        [Route("DynamicBehaviour")]
        public async Task<object> DynamicBehaviour()
        {
           
            var model = await _binArticleServices.DynamicBehaviour();
            var data = new { success = true, data = model };
            return data;
        }



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
