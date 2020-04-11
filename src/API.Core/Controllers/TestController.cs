using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Common.Helper;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly IBinServices _binArticleServices;
       

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="BindvertisementServices">注册bin服务</param>
       
        public TestController(IBinServices BindvertisementServices)
        {
          
            _binArticleServices = BindvertisementServices;
        }



        /// <summary>
        /// 查询bin模板
        /// </summary>
        /// <returns></returns>
        // GET: api/Test
        [HttpGet]
        [Route("BinTemplate")]
        public async Task<object> GetBinTemplate()
        {
            //调用该方法，这里 _blogArticleServices 是依赖注入的实例，不是类
            var model = await _binArticleServices.GetBinTemplate();
            var data = new { success = true, data = model };
            return data;
        }


        ///// <summary>
        ///// 获取Bin列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("binInfo")]
        //public async Task<MessageModel<BinInfoViewModels>> GetBinInfo() 
        //{
        //    var model = await _binArticleServices.GetBinList();

        //    return new MessageModel<BinInfoViewModels>()
        //    {
        //        Msg = "获取成功",
        //        Success = true,
        //        Code = 0,
        //        Response = model
        //    };
        //}

        ///// <summary>
        ///// 查询所有
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<List<BinInfo>> GetAll()
        //{
        //    // 分页查询 
        //    var model = await _binArticleServices.Query(0,10);
        //    return model;
        //}

      

        ///// <summary>
        ///// 动态表达式调取
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("DynamicBehaviour")]
        //public async Task<object> DynamicBehaviour()
        //{
           
        //    var model = await _binArticleServices.DynamicBehaviour();
        //    var data = new { success = true, data = model };
        //    return data;
        //}


        ///// <summary>
        ///// 添加操作
        ///// </summary>
        ///// <param name="binInfo"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("AddBin")]
        //public async Task<MessageModel<string>> AddBin([FromBody] BinInfo binInfo) 
        //{
        //    var data = new MessageModel<string>();
        //    binInfo.CreateTime= DateTime.Now;

        //    var id = (await _binArticleServices.Add(binInfo));
        //    data.Success = id > 0;
            
        //    if (data.Success)
        //    {
        //        data.Code = (int)CodeStatus.Success;
        //        data.Response = id.ObjToString();
        //        data.Msg = "添加成功";
        //    }

        //    return data;
        //}



       


    }
}
