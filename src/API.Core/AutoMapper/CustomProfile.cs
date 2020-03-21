using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using AutoMapper;

namespace API.Core.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BinInfo, BinInfoViewModels>();
        }
    }
}
