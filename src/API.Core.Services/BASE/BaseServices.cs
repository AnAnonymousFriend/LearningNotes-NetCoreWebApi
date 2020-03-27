using API.Core.IRepository.BASE;
using API.Core.IServices.BASE;
using API.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Core.Services.BASE
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> baseDal;

        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 根据ID查询 返回实体
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await baseDal.QueryById(objId);
        }

        public async Task<TEntity> QueryById(object objId, bool blnUseCache)
        {
            return await baseDal.QueryById(objId, blnUseCache);
        }

        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await baseDal.QueryByIDs(lstIds);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(whereExpression, orderByExpression, isAsc);
        }


        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, strOrderByFileds);
        }
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFileds);
        }
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }
        public async Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TEntity>> Query(int intPageIndex, int intPageSize)
        {
            return await baseDal.Query(intPageIndex, intPageSize);
        }
        public async Task<List<TEntity>> LeagueQueryPage(Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex = 0, int intPageSize = 10, string strOrderByFileds = null)
        {
            return await baseDal.LeagueQueryPage(whereExpression, intPageIndex, intPageSize, strOrderByFileds);
        }
        public async Task<List<TEntity>> LeagueQueryAll(DoubleTable doubleTable)
        {
            return await baseDal.LeagueQueryAll(doubleTable);
        }
        public async Task<List<TEntity>> LeagueQueryPage(DoubleTable doubleTable)
        {
            return await baseDal.LeagueQueryPage(doubleTable);
        }
        public async Task<List<TEntity>> LeagueQueryPage(Expression<Func<TEntity, bool>> whereExpression, DoubleTable doubleTable)
        {
            return await baseDal.LeagueQueryPage(whereExpression, doubleTable);
        }
        public async Task<List<TEntity>> LeagueTables(LeagueTables leagueTables)
        {
            return await baseDal.LeagueTables(leagueTables);
        }


        public async Task<int> Add(TEntity model)
        {
            return await baseDal.Add(model);
        }
        public async Task<int> AddList(List<TEntity> insertObjs)
        {
            return await baseDal.AddList(insertObjs);
        }


        public async Task<bool> Update(TEntity model)
        {
            return await baseDal.Update(model);
        }


        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }


        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            return await baseDal.Update(strSql, parameters);
        }
        public async Task<bool> UpdateList(List<TEntity> list)
        {
            return await baseDal.UpdateList(list);
        }


        public async Task<bool> Delete(TEntity model)
        {
            return await baseDal.Delete(model);
        }

        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }



        public async Task<List<TEntity>> QuerySQL(string sql)
        {
            return await baseDal.QuerySQL(sql);
        }

        public async Task<TEntity[]> SqlByArray(string sql)
        {
            return await baseDal.SqlByArray(sql);
        }


        #region 扩展方法
        public async Task<List<TEntity>> DynamicWhereByLits(Dictionary<string, string> pairs)
        {
            return await baseDal.DynamicWhereByLits(pairs);
        }

        public async Task<List<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.GetEntitiesAsync(whereExpression);
        }
        public async Task<List<TEntity>> DynamicWhereByList(Dictionary<string, string> pairs)
        {
            return await baseDal.DynamicWhereByList(pairs);
        }

        #endregion





    }

}
