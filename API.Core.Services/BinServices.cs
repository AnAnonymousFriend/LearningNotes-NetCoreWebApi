using API.Core.Common.Helper;
using API.Core.Common.Redis;
using API.Core.IRepository;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using API.Core.Services.BASE;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public BinServices(IBinArticleRepository dal, IMapper mapper,IRedisCacheManager redisCacheManager)
        {
            this._dal = dal;
            base.baseDal = dal;
            this._mapper = mapper;
            _redisCacheManager = redisCacheManager;

        }

        public async Task<BinInfoViewModels> GetBinList()
        {
            var blogArticle = (await Query(a => a.Id == 1)).FirstOrDefault();
            BinInfoViewModels models = _mapper.Map<BinInfoViewModels>(blogArticle);
            return models;
        }



        public async Task<List<BinInfo>> TestGetBinList()
        {
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

      
    }
}
