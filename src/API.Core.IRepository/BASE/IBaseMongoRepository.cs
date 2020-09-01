using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.IRepository.BASE
{
    public interface IBaseMongoRepository<TEntity> where TEntity : class
    {

        object Query();

        BsonDocument QueryById(string key, string value);

        List<BsonDocument> QueryListById(string key, string value);

        bool AddList(BsonDocument entity);

        bool DeleteByID(string key, string value);

        bool Update(string key, string value, string value2);

    }
}
