using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Rita.System.UnitOfWork
{
    /// <summary>
    /// Repository基类
    /// </summary>
    /// <remarks>
    /// Description :   创建加密帮助类
    /// -------------------------------------------
    /// </remarks>
    public class GenericRepository<T, CTX> : IRepository<T>
        where T : class, IStored
        where CTX : DbContext
    {
        CTX dbContext;
        public GenericRepository(CTX context)
        {
            dbContext = context;
        }
        public virtual void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(int key)
        {
            var entity = dbContext.Set<T>().Find(key);
            Delete(entity);
        }

        public virtual T Find(int key)
        {
            return dbContext.Set<T>().Find(key);
        }
        public virtual void Delete(T entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Deleted)
            {
                dbContext.Set<T>().Attach(entity);
            }
            dbContext.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, PagedCondition pagion = null, string include = "")
        {
            IQueryable<T> queryable = dbContext.Set<T>();
            if (!string.IsNullOrEmpty(include))
            {
                var pathes = include.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var path in pathes)
                {
                    queryable = queryable.Include(path);
                }
            }
            if (null != filter)
            {
                queryable = queryable.Where(filter);
            }
            if (null != orderBy)
            {
                var orderedResult = orderBy(queryable);
                if (null != pagion)
                {
                    pagion.RecordCount = orderedResult.Count();
                    return orderedResult.Skip((pagion.PageIndex - 1) * pagion.PageSize).Take(pagion.PageSize);
                }
                return orderedResult;
            }
            return queryable;
        }

        public bool Any(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> queryable = dbContext.Set<T>();
            if(null==predicate)
            {
                return queryable.Any();
            }
            return queryable.Any(predicate);
        }

        public virtual void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
