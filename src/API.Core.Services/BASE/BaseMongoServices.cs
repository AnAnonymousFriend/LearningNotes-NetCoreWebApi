using API.Core.IRepository.BASE;
using API.Core.IServices.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services.BASE
{
    public class BaseMongoServices<TEntity> : IBaseMongoServices<TEntity> where TEntity : class, new()
    {
        public IBaseMongoRepository<TEntity> BaseDal;

    }
}
