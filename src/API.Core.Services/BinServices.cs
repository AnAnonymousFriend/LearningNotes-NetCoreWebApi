﻿using API.Core.Common.Redis;
using API.Core.IRepository;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using API.Core.Services.BASE;
using API.Core.Services.ModulePlant;
using API.Core.Services.ModulePlant.Factory;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Services
{
    public class BinServices : BaseServices<BinInfo>, IBinServices
    {
        IBinArticleRepository _dal;
        IMapper _mapper;
        IRedisCacheManager _redisCacheManager;
        public BinServices(IBinArticleRepository dal, IMapper mapper, IRedisCacheManager redisCacheManager)
        {
            _dal = dal;
            baseDal = dal;
            _mapper = mapper;
            _redisCacheManager = redisCacheManager;

        }

        // id查询
        public async Task<BinInfoViewModels> GetBinList(int id)
        {
            var binArticle = (await Query(a => a.Id == id)).FirstOrDefault();
            BinInfoViewModels models = _mapper.Map<BinInfoViewModels>(binArticle);
            return models;
        }

        // 分页查询
        public async Task<BinInfoViewModels> GetBinList() 
        {
            //var a = await GetSql().;
            var binArticle = await Query(1, 10);
            BinInfoViewModels models = _mapper.Map<BinInfoViewModels>(binArticle);
            return models;

        }


        public async Task<object> GetSql()
        {
            string sql = "select id from bin_Info LIMIT 0,5";
            return await SqlByArray(sql);
        }



        public async Task<List<BinInfo>> TestGetBinList(int modelid,string compatibleType)
        {

            string modelTypeSQL = "select id from bin_madalena_type where  madalena_id=1 and compatible_type='IBM' LIMIT 1";
            var modelTypeid = await SqlByArray(modelTypeSQL);
            return null;


            //throw new ArgumentNullException("发生异常");

            //DoubleTable doubleTable = new DoubleTable
            //{
            //    LeftSurface = "BinInfo",
            //    RightSurface = "OrderInfo",
            //    RightKey = "Id",
            //    ForeignKey = "order_id",
            //    QueryField = new string[]
            //    {
            //        "Pn",
            //        "Sn"
            //    }
            //};
            //List<BinInfo> binInfos = new List<BinInfo>();

            //if (_redisCacheManager.Get<object>("Redis.Bin") != null)
            //{
            //    binInfos = _redisCacheManager.Get<List<BinInfo>>("Redis.Bin");
            //}
            //else
            //{
            //    binInfos = await FedExPage(doubleTable);
            //    _redisCacheManager.Set("Redis.Bin", binInfos, TimeSpan.FromHours(2));
            //}
            //return binInfos;

        }


        // 动态条件查询
        public async Task<List<BinInfo>> DynamicBehaviour()
        {
            //OrIF中第一个参数为bool类型 可写判断
            // 如OrIF(a == b, it => it.Id == 1)
            // 如果变量a与b相等则添加 id== 1 的条件语句
            var exp = SqlSugar.Expressionable.Create<BinInfo>()
                                             .OrIF(true, it => it.Id == 18)
                                             .And(it => it.BinType == "SFP")
                                             .ToExpression();
            var list = await GetEntitiesAsync(exp);
            return null;


        }




        /// <summary>
        ///  抽象工厂类
        /// </summary>
        public void CreateBinFile()
        {
            // box 找码逻辑
            AbstractFactory boxfactory = new BoxFactory();
            BInFile boxbinFile = boxfactory.CreateBinFile();
            boxbinFile.CreateBinFile();

            // os 找码逻辑
            AbstractFactory osfactory = new OsFactory();
            BInFile osbinFile = osfactory.CreateBinFile();
            osbinFile.CreateBinFile();
        }

        public async Task<object> GetBinTemplate()
        {
            string modelTypeSQL = "select id from bin_madalena_type where  madalena_id=1 and compatible_type='IBM' LIMIT 1";
            var modelTypeid = SqlByArray(modelTypeSQL);

            string sql = "SELECT bin_template,version,sn FROM bin_madalena_attr_value WHERE madalena_type_id=1 and attr_key='0,0' AND attr_value='0,0' AND version='AOC-1'";
            var bins = await SqlByArray(modelTypeSQL);

            return bins;
        }
    }
}
