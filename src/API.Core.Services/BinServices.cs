using API.Core.Common.Redis;
using API.Core.IRepository;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using API.Core.Services.BASE;
using AutoMapper;
using System;
using System.Collections.Generic;
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
            this._dal = dal;
            base.baseDal = dal;
            this._mapper = mapper;
            _redisCacheManager = redisCacheManager;

        }

        public async Task<BinInfoViewModels> GetBinList()
        {
            //var binArticle = (await Query(a => a.Id == 1)).FirstOrDefault();

            //BinInfoViewModels models = _mapper.Map<BinInfoViewModels>(binArticle);
            //return models;

            var binInFoList = await Query("10", 0, 10, "Id");
            BinInfoViewModels models = _mapper.Map<BinInfoViewModels>(binInFoList);
            return models;
        }
       


        public async Task<List<BinInfo>> TestGetBinList()
        {

            throw new ArgumentNullException("发生异常");

            DoubleTable doubleTable = new DoubleTable
            {
                LeftSurface = "BinInfo",
                RightSurface = "OrderInfo",
                RightKey = "Id",
                ForeignKey = "order_id",
                QueryField = new string[]
                {
                    "Pn",
                    "Sn"
                }
            };
            List<BinInfo> binInfos = new List<BinInfo>();

            if (_redisCacheManager.Get<object>("Redis.Bin") != null)
            {
                binInfos = _redisCacheManager.Get<List<BinInfo>>("Redis.Bin");
            }
            else
            {
                binInfos = await FedExPage(doubleTable);
                _redisCacheManager.Set("Redis.Bin", binInfos, TimeSpan.FromHours(2));
            }
            return binInfos;

        }

       

        public async Task<List<BinInfo>> DynamicBehaviour()
        {
            // OrIF中第一个参数为bool类型 可写判断
            // 如OrIF(a==b,it => it.Id == 1) 
            // 如果变量a与b相等则添加 id==1 的条件语句
            var exp = SqlSugar.Expressionable.Create<BinInfo>()
                                             .OrIF(true, it => it.Id == 1)
                                             .And(it => it.BinType == "SFP")
                                             .ToExpression();
            var list =  await GetEntitiesAsync(exp);
            return list;


        }



        public async void CreateBinFile() 
        {
        
        }

    }
}
