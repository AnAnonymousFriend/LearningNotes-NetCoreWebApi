using API.Core.Common.Helper;
using API.Core.IRepository.BASE;
using API.Core.Model;
using API.Core.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Core.Repository.BASE
{
    namespace Blog.Core.Repository.Base
    {
        public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
        {
            public DbContext Context { get; set; }
            internal SqlSugarClient Db { get; private set; }
            internal SimpleClient<TEntity> EntityDB { get; private set; }
            public BaseRepository()
            {
                DbContext.Init(BaseDBConfig.ConnectionString);
                Context = DbContext.GetDbContext();
                Db = Context.Db;
                EntityDB = Context.GetEntityDB<TEntity>(Db);
            }



            public async Task<TEntity> QueryByID(object objId)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().InSingle(objId));
            }

            /// <summary>
            /// 功能描述:根据ID查询一条数据
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
            /// <param name="blnUseCache">是否使用缓存</param>
            /// <returns>数据实体</returns>
            public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
            }

            /// <summary>
            /// 功能描述:根据ID查询数据
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
            /// <returns>数据实体列表</returns>
            public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().In(lstIds).ToList());
            }

            /// <summary>
            /// 写入实体数据
            /// </summary>
            /// <param name="entity">博文实体类</param>
            /// <returns></returns>
            public async Task<int> Add(TEntity entity)
            {
                var i = await Task.Run(() => Db.Insertable(entity).ExecuteReturnBigIdentity());
                //返回的i是long类型,这里你可以根据你的业务需要进行处理
                return (int)i;
            }

            /// <summary>
            /// 批量插入
            /// 注意 ： SqlSever 建表语句带有Wtih(设置)，如果设置不合理，可能会引起慢，把With删掉就会很快
            /// </summary>
            /// <param name="insertObjs"></param>
            /// <returns></returns>
            public async Task<int> AddList(List<TEntity> insertObjs) 
            {
                var i = await Task.Run(() => Db.Insertable(insertObjs.ToArray()).ExecuteReturnBigIdentity());
                return (int)i;   
            }



            /// <summary>
            /// 更新实体数据
            /// </summary>
            /// <param name="entity">实体类</param>
            /// <returns></returns>
            public async Task<bool> Update(TEntity entity)
            {
                //这种方式会以主键为条件
                var i = await Task.Run(() => Db.Updateable(entity).ExecuteCommand());
                return i > 0;
            }

            public async Task<bool> Update(TEntity entity, string strWhere)
            {
                return await Task.Run(() => Db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
            }

            public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
            {
                return await Task.Run(() => Db.Ado.ExecuteCommand(strSql, parameters) > 0);
            }

            [Obsolete]
            public async Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "" )   
            {
                IUpdateable<TEntity> up = await Task.Run(() => Db.Updateable(entity));
                if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
                {
                    up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
                }
                if (lstColumns != null && lstColumns.Count > 0)
                {
                    up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
                }
                if (!string.IsNullOrEmpty(strWhere))
                {
                    up = await Task.Run(() => up.Where(strWhere));
                }
                return await Task.Run(() => up.ExecuteCommand()) > 0;
            }

            /// <summary>
            /// 根据实体删除一条数据
            /// </summary>
            /// <param name="entity">博文实体类</param>
            /// <returns></returns>
            public async Task<bool> Delete(TEntity entity)
            {
                var i = await Task.Run(() => Db.Deleteable(entity).ExecuteCommand());
                return i > 0;
            }

            /// <summary>
            /// 删除指定ID的数据
            /// </summary>
            /// <param name="id">主键ID</param>
            /// <returns></returns>
            public async Task<bool> DeleteById(object id)
            {
                var i = await Task.Run(() => Db.Deleteable<TEntity>(id).ExecuteCommand());
                return i > 0;
            }

            /// <summary>
            /// 删除指定ID集合的数据(批量删除)
            /// </summary>
            /// <param name="ids">主键ID集合</param>
            /// <returns></returns>
            public async Task<bool> DeleteByIds(object[] ids)
            {
                var i = await Task.Run(() => Db.Deleteable<TEntity>().In(ids).ExecuteCommand());
                return i > 0;
            }



            /// <summary>
            /// 功能描述:查询所有数据
            /// 作　　者:Blog.Core
            /// </summary>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query()
            {
                return await Task.Run(() => EntityDB.GetList());
            }

            /// <summary>
            /// 功能描述:查询数据列表
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="strWhere">条件</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(string strWhere)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            }

            /// <summary>
            /// 功能描述:查询数据列表
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="whereExpression">whereExpression</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
            {
                return await Task.Run(() => EntityDB.GetList(whereExpression));
            }

            /// <summary>
            /// 功能描述:查询一个列表
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="whereExpression">条件表达式</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList());
            }
            /// <summary>
            /// 功能描述:查询一个列表
            /// </summary>
            /// <param name="whereExpression"></param>
            /// <param name="orderByExpression"></param>
            /// <param name="isAsc"></param>
            /// <returns></returns>
            public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
            }

            /// <summary>
            /// 功能描述:查询一个列表
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="strWhere">条件</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
            }


            /// <summary>
            /// 功能描述:查询前N条数据
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="whereExpression">条件表达式</param>
            /// <param name="intTop">前N条</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(
                Expression<Func<TEntity, bool>> whereExpression,
                int intTop,
                string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
            }

            /// <summary>
            /// 功能描述:查询前N条数据
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="strWhere">条件</param>
            /// <param name="intTop">前N条</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(
                string strWhere,
                int intTop,
                string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>()
                      .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                      .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                      .Take(intTop).ToList());
            }



            /// <summary>
            /// 功能描述:分页查询
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="whereExpression">条件表达式</param>
            /// <param name="intPageIndex">页码（下标0）</param>
            /// <param name="intPageSize">页大小</param>
            /// <param name="intTotalCount">数据总量</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(
                Expression<Func<TEntity, bool>> whereExpression,
                int intPageIndex,
                int intPageSize,
                string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
            }

            /// <summary>
            /// 功能描述:分页查询
            /// 作　　者:Blog.Core
            /// </summary>
            /// <param name="strWhere">条件</param>
            /// <param name="intPageIndex">页码（下标0）</param>
            /// <param name="intPageSize">页大小</param>
            /// <param name="intTotalCount">数据总量</param>
            /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
            /// <returns>数据列表</returns>
            public async Task<List<TEntity>> Query(
              string strWhere,
              int intPageIndex,
              int intPageSize,
              string strOrderByFileds)
            {
                return await Task.Run(() => Db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
            }




            public async Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
            {
                return await Task.Run(() => Db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .WhereIF(whereExpression != null, whereExpression)
                .ToPageList(intPageIndex, intPageSize));
            }



            /// <summary>
            /// 执行SQL查询语句
            /// </summary>
            /// <param name="sql">执行sql语句查询</param>
            /// <returns>返回TEntity实体集合</returns>
            public async Task<List<TEntity>> QuerySQL(string sql)
            {
                return await Task.Run(() => Db.SqlQueryable<TEntity>(sql).ToList());
            }

            /// <summary>
            /// 双联表查询全部
            /// </summary>
            /// <param name="doubleTable"></param>
            /// <returns></returns>
            public async Task<List<TEntity>> FedEx(DoubleTable doubleTable)
            {
                string relation = $"s1.{doubleTable.ForeignKey} = s2.{doubleTable.RightKey}";
                return await Task.Run(() => Db.Queryable(doubleTable.LeftSurface, "s1")
                                              .AddJoinInfo(doubleTable.RightSurface, "s2", relation)
                                              .Select<TEntity>(MonogramHelper.GetQueryField(doubleTable.QueryField))
                                              .OrderByIF(!string.IsNullOrEmpty(doubleTable.OrderByFileds), doubleTable.OrderByFileds)
                                              .ToList()
               );
            }

            /// <summary>
            /// 联表查询分页
            /// </summary>
            /// <param name="doubleTable"></param>
            /// <returns></returns>
            public async Task<List<TEntity>> FedExPage(DoubleTable doubleTable)
            {
                string relation = $"s1.{doubleTable.ForeignKey} = s2.{doubleTable.RightKey}";
                return await Task.Run(() => Db.Queryable(doubleTable.LeftSurface, "s1")
                                              .AddJoinInfo(doubleTable.RightSurface, "s2", relation)
                                              .Select<TEntity>(MonogramHelper.GetQueryField(doubleTable.QueryField))
                                              .OrderByIF(!string.IsNullOrEmpty(doubleTable.OrderByFileds), doubleTable.OrderByFileds)
                                              .ToPageList(doubleTable.IntPageIndex, doubleTable.IntPageSize)
               );
            }

            /// <summary>
            /// 联表查询 
            /// 默认查询全部 没有做查询字段优化
            /// </summary>
            /// <param name="whereExpression">条件表达式</param>
            /// <param name="doubleTable">联表参数</param>
            /// <returns></returns>
            public async Task<List<TEntity>> FedExPage(Expression<Func<TEntity, bool>> whereExpression, DoubleTable doubleTable)
            {
                string relation = $"s1.{doubleTable.ForeignKey} = s2.{doubleTable.RightKey}";
                return await Task.Run(() => Db.Queryable(doubleTable.LeftSurface, "s1")
                                              .AddJoinInfo(doubleTable.RightSurface, "s2", relation)
                                              .Select<TEntity>("*")
                                              .OrderByIF(!string.IsNullOrEmpty(doubleTable.OrderByFileds), doubleTable.OrderByFileds)
                                              .WhereIF(whereExpression != null, whereExpression)
                                              .ToPageList(doubleTable.IntPageIndex, doubleTable.IntPageSize)
               );
            }

            /// <summary>
            /// 查询行数
            /// </summary>
            /// <param name="whereExpression">查询条件</param>
            /// <returns></returns>
            public async Task<int> TableCount(Expression<Func<TEntity, bool>> whereExpression) 
            {
                return await Task.Run(() => Db.Queryable<TEntity>()
                                              .WhereIF(whereExpression != null, whereExpression)
                                              .Count());

            }


            /// <summary>
            /// 查询单表 动态where条件
            /// </summary>
            /// <param name="valuePairs">动态where条件</param>
            /// <returns></returns>
            public async Task<List<TEntity>> DynamicWhereByLits(Dictionary<string, string> pairs)
            {
                List<IConditionalModel> conModels = new List<IConditionalModel>();
                foreach (var item in pairs)
                {
                    conModels.Add(new ConditionalModel() { FieldName = item.Key, ConditionalType = ConditionalType.Equal, FieldValue = item.Value });
                }
                return await Task.Run(() => Db.Queryable<TEntity>().Where(conModels).ToList());
            }


            // 动态拼接Lambda
            public async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpression) 
            {
                return await Task.Run(() => Db.Queryable<TEntity>().Where(whereExpression).ToList());
            }

            /// <summary>
            /// 动态查询表达式
            /// </summary>
            /// <param name="pairs"></param>
            /// <returns></returns>
            public async Task<List<TEntity>> DynamicWhereByList(Dictionary<string, string> pairs)
            {
                List<IConditionalModel> conModels = new List<IConditionalModel>();
                foreach (var item in pairs)
                {
                    conModels.Add(new ConditionalModel()
                    {
                        FieldName = item.Key,
                        ConditionalType = ConditionalType.Equal,
                        FieldValue = item.Value
                    });
                }
                var strsql = await Task.Run(() => Db.Queryable<TEntity>().Where(conModels).ToSql().ToString());
                return await Task.Run(() => Db.Queryable<TEntity>().Where(conModels).ToList());
            }

            /// <summary>
            /// 查询行数
            /// </summary>
            /// <param name="whereExpression">查询条件</param>
            /// <returns></returns>
            public async Task<int> QueryCount(string strWhere)
            {
                return await Task.Run(() => Db.Queryable<TEntity>()
                                            .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Count());
            }



            /// <summary>
            /// 批量修改
            /// </summary>
            /// <param name="list"></param>
            /// <returns></returns>
            public async Task<bool> UpdateList(List<TEntity> list)
            {
                return await Task.Run(() => Db.Updateable(list).ExecuteCommand() > 0 ? true : false);
            }

            /// <summary>
            /// 批量添加
            /// </summary>
            /// <param name="entities"></param>
            /// <returns></returns>
            public async Task<int> AddListEntity(List<TEntity> entities)
            {
                return await Task.Run(() => Db.Insertable(entities.ToArray()).ExecuteCommand());
            }



        }
    }


}
