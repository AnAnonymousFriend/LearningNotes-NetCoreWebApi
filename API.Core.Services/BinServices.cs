using API.Core.IRepository;
using API.Core.IServices;
using API.Core.Model;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using API.Core.Services.BASE;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Services
{
    public class BinServices : BaseServices<BinInfo>, IBinServices
    {
        IBinArticleRepository _dal;
        IMapper _mapper;

        public BinServices(IBinArticleRepository dal, IMapper mapper)
        {
            this._dal = dal;
            base.baseDal = dal;
            this._mapper = mapper;

        }

        public async Task<BinInfoViewModels> GetBinList()
        {
            var blogArticle = (await base.Query(a => a.Id == 1)).FirstOrDefault();
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
            return await FedExPage(doubleTable);

        }


    }
}
