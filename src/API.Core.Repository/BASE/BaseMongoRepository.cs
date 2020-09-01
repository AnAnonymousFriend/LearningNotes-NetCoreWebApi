using API.Core.Common.Helper;
using API.Core.IRepository.BASE;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Repository.BASE
{
    public class BaseMongoRepository<TEntity> : IBaseMongoRepository<TEntity> where TEntity : class, new()
    {

        private readonly IMongoCollection<BsonDocument> _collection;   //数据表操作对象
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        /// <param name="tableName">表名</param>
        public BaseMongoRepository(string tableName)
        {

            //获取连接字符串
            string mongoConfiguration = Appsettings.app(new string[] { "AppSettings", "MongoCaching", "ConnectionString" });
            //获取数据库名
            string dbName = Appsettings.app(new string[] { "AppSettings", "MongoCaching", "DbName" });

            var client = new MongoClient(mongoConfiguration);

            var database = client.GetDatabase(dbName);

            //获取对特定数据表集合中的数据的访问
            _collection = database.GetCollection<BsonDocument>(tableName);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public object Query()
        {
            return _collection.Find(T => true).ToList();
        }

        /// <summary>
        /// 根据ID 查询一条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public BsonDocument QueryById(string key, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);
            var document = _collection.Find(filter).First();
            return document;
        }

        /// <summary>
        /// 查询一组数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<BsonDocument> QueryListById(string key, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);
            var document = _collection.Find(filter).ToList();
            return document;
        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddList(BsonDocument entity)
        {
            _collection.InsertOne(entity);
            return true;
        }

        /// <summary>
        /// 删除某一个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool DeleteByID(string key, string value)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);
            _collection.DeleteOne(filter);
            return true;

        }



        /// <summary>
        /// 修改值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public bool Update(string key, string value, string value2)
        {
            var filter = Builders<BsonDocument>.Filter.Eq(key, value);
            var update = Builders<BsonDocument>.Update.Set(key, value2);
            _collection.UpdateOne(filter, update);
            return true;
        }
    }
}
