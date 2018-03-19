using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rita.System.UnitOfWork
{
    /// <summary>
    /// 公用仓储接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IRepository<T>
        where T : class, IStored
    {
        /// <summary>
        /// 获取所有实体集
        /// </summary>
        /// <param name="filter">筛选条件</param>
        /// <param name="orderBy">排序函数</param>
        /// <param name="pagion">分页条件，一般分页和orderBy同时使用</param>
        /// <param name="include">预加载的路径</param>
        /// <returns>满足条件的实体集信息</returns>
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                          PagedCondition pagion = null,
                          string include = "");
        /// <summary>
        /// 判断是否存在满足条件的任何对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> predicate = null);
        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// 删除实体信息
        /// </summary>
        /// <param name="key">要删除的实体键</param>
        void Delete(int key);
        /// <summary>
        /// 新增实体信息
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        T Find(int key);
    }
}
