using System;
using System.Data.Entity;

namespace Rita.System.UnitOfWork
{
    /// <summary>
    /// 工作单元类
    /// </summary>
    public class UnitOfWork<C> : IDisposable
        where C : DbContext
    {
        C dbContext;
        public UnitOfWork(C context)
        {
            dbContext = context;
        }
        /// <summary>
        /// 根据Entity类型创建对应的实体仓储
        /// </summary>
        /// <typeparam name="T">Entity类型</typeparam>
        /// <returns></returns>
        public IRepository<T> CreateRepository<T>()
            where T : class, IStored
        {
            return new GenericRepository<T, C>(dbContext);
        }
        /// <summary>
        /// 提交更新至数据库
        /// </summary>
        public void Commit()
        {
            dbContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    ((IDisposable)dbContext)?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        ~UnitOfWork()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
