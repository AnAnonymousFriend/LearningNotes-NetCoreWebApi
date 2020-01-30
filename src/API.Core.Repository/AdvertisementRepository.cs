using API.Core.IRepository;
using API.Core.Model.Models;
using API.Core.Model.ViewModels;
using API.Core.Repository.BASE;
using API.Core.Repository.BASE.Blog.Core.Repository.Base;
using API.Core.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Core.Repository
{
    public class AdvertisementRepository : BaseRepository<BinInfo>, IAdvertisementRepository
    {
    }
}
