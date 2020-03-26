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
        Task<List<TEntity>> Query(string strWhere);
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);
        Task<List<TEntity>> Query(int intPageIndex, int intPageSize);
        Task<List<TEntity>> LeagueQueryPage(Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex = 0, int intPageSize = 10, string strOrderByFileds = null);
        Task<List<TEntity>> LeagueQueryAll(DoubleTable doubleTable);
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


        #region 扩展方法
        Task<List<TEntity>> DynamicWhereByLits(Dictionary<string, string> pairs);

        Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> DynamicWhereByList(Dictionary<string, string> pairs);

        #endregion


    }
}
