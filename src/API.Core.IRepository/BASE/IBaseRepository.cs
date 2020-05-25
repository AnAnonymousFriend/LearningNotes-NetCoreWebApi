using API.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Core.IRepository.BASE
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {



        Task<List<TEntity>> Query();

        Task<List<TOutput>> Query<TOutput>(Expression<Func<TOutput, bool>> whereExp, Expression<Func<TOutput, TOutput>> selectExp);

        Task<List<TEntity>> Query(string strWhere);
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);

        Task<List<TEntity>> QueryByIDs(int[] lstIds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        Task<List<TEntity>> Query(int intPageIndex, int intPageSize);
        Task<List<TEntity>> LeagueQueryPage(Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex = 0, int intPageSize = 10, string strOrderByFileds = null);
        Task<List<TEntity>> LeagueQueryAll(DoubleTable doubleTable);

        Task<List<TOutput>> LeagueQueryAll<TOutput>(Expression<Func<TOutput, bool>> whereExp, DoubleTable doubleTable);
        Task<List<TEntity>> LeagueQueryPage(DoubleTable doubleTable);
        Task<List<TEntity>> LeagueQueryPage(Expression<Func<TEntity, bool>> whereExpression, DoubleTable doubleTable);
        Task<List<TEntity>> LeagueTables(LeagueTables leagueTables);


        Task<int> Add(TEntity model);
        Task<int> AddList(List<TEntity> insertObjs);


        Task<bool> Update(TEntity model);
        Task<bool> Update(TEntity entity, string strWhere);
        Task<bool> Update(string strSql, SugarParameter[] parameters = null);
        Task<bool> UpdateList(List<TEntity> list);


        Task<bool> Delete(TEntity model);

        Task<bool> DeleteById(object id);

        Task<bool> DeleteByIds(object[] ids);



        Task<List<TEntity>> QuerySQL(string sql);

        Task<TEntity[]> SqlByArray(string sql);

        Task<List<dynamic>> DynamicSqlList(string sql);
        Task<dynamic> DynamicSql(string sql);

        #region 扩展方法
        Task<List<TEntity>> DynamicWhereByLits(Dictionary<string, string> pairs);

        Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> DynamicWhereByList(Dictionary<string, string> pairs);


        /// <summary>
        /// 双表连接查询数据
        /// </summary>
        /// <typeparam name="T2">B表类型</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param>
        /// <returns></returns>
        Task<List<TOutput>> Query<T2, TOutput>(Expression<Func<TEntity, T2, bool>> joinExp, Expression<Func<TEntity, T2, bool>> whereExp, Expression<Func<TEntity, T2, TOutput>> selectExp);



        /// <summary>
        /// 三表连接查询
        /// </summary>
        /// <typeparam name="T2">第2张表</typeparam>
        /// <typeparam name="T3">第3张表</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param>
        /// <returns></returns>
        Task<List<TOutput>> Query<T2, T3, TOutput>(Expression<Func<TEntity, T2, T3, bool>> joinExp, Expression<Func<TEntity, T2, T3, bool>> whereExp, Expression<Func<TEntity, T2, T3, TOutput>> selectExp);


        /// <summary>
        /// 双表连接查询数据
        /// </summary>
        /// <typeparam name="T2">B表类型</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param>
        /// <param name="inExp">in表达式</param>
        /// <param name="inValues">in的范围</param>
        /// <returns></returns>
        Task<List<TOutput>> QueryByIn<T2, TOutput>(Expression<Func<TEntity, T2, bool>> joinExp, Expression<Func<TEntity, T2, bool>> whereExp, Expression<Func<TEntity, T2, TOutput>> selectExp, Expression<Func<TEntity, T2, object>> inExp, object inValues);

        /// <summary>
        /// 三表连接查询加In
        /// </summary>
        /// <typeparam name="T2">第2张表</typepar--am>
        /// <typeparam name="T3">第3张表</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param>
        /// <param name="inExp">in表达式</param>-
        /// <param name="inValues">in的范围</param>
        /// <returns></returns>
        Task<List<TOutput>> QueryByIn<T2, T3, TOutput>(Expression<Func<TEntity, T2, T3, bool>> joinExp, Expression<Func<TEntity, T2, T3, bool>> whereExp, Expression<Func<TEntity, T2, T3, TOutput>> selectExp, Expression<Func<TEntity, T2, object>> inExp, object inValues);

        /// <summary>
        /// 2表查询带排序
        /// </summary>
        /// <typeparam name="T2">B表类型</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param> 
        /// <param name="orderExp">排序表达式(选取排序字段）</param>
        /// <param name="orderByType">排序方式</param>
        /// <returns></returns>
        Task<List<TOutput>> QueryByOrder<T2, TOutput>(Expression<Func<TEntity, T2, bool>> joinExp, Expression<Func<TEntity, T2, bool>> whereExp, Expression<Func<TEntity, T2, TOutput>> selectExp, Expression<Func<TEntity, T2, object>> orderExp, OrderByType orderByType);

        /// <summary>
        /// 三表查询带排序
        /// </summary>
        /// <typeparam name="T2">第2张表</typepar--am>
        /// <typeparam name="T3">第3张表</typeparam>
        /// <typeparam name="TOutput">返回类型</typeparam>
        /// <param name="joinExp">join表达式</param>
        /// <param name="whereExp">where表达式</param>
        /// <param name="selectExp">select表达式</param> 
        /// <param name="orderExp">排序表达式(选取排序字段）</param>
        /// <param name="orderByType">排序方式</param>
        /// <returns></returns>
        Task<List<TOutput>> QueryByOrder<T2, T3, TOutput>(Expression<Func<TEntity, T2, T3, bool>> joinExp, Expression<Func<TEntity, T2, T3, bool>> whereExp, Expression<Func<TEntity, T2, T3, TOutput>> selectExp, Expression<Func<TEntity, T2, object>> orderExp, OrderByType orderByType);
        #endregion




    }
}
