using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using Rita.System.UnitOfWork;

namespace Rita.System
{
    /// <summary>
    /// 系统扩展类
    /// </summary>
    /// <remarks>
    /// Description :   创建系统扩展类
    /// -------------------------------------------
    /// </remarks>
    public static class Extensions
    {
        /// <summary>
        /// 从Object类型转换为Entity类型
        /// </summary>
        /// <typeparam name="F">起始类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="fromEntity">起始项目</param>
        /// <returns></returns>
        public static T CloneTo<F, T>(this F fromEntity)
            where T : class, IStored, new()
        {
            var rslt = new T();
            var fromProperties = typeof(F).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static); ;
            var destProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //克隆属性
            foreach (var destProperty in destProperties)
            {
                if (fromProperties.Any(p => string.Equals(p.Name, destProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                {
                    var property = fromProperties.First(p => p.Name.Equals(destProperty.Name, StringComparison.CurrentCultureIgnoreCase));
                    destProperty.SetValue(rslt, property.GetValue(fromEntity));
                }
            }
            return rslt;
        }
        /// <summary>
        /// 根据目标类型动态生成SQL语句的Select部分，供查询使用
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <returns></returns>
        static string GenerateSelectSQL<T>()
        {
            var sqlBuilder = new StringBuilder("new(");
            var destProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //克隆属性
            foreach (var destProperty in destProperties)
            {
                sqlBuilder.AppendFormat("{0},", destProperty.Name);
            }
            sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
            sqlBuilder.Append(")");
            return sqlBuilder.ToString();
        }
        /// <summary>
        /// 从dynamicobject转换为目标类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="v">dynamicObject</param>
        /// <returns></returns>
        static T ConvertTo<T>(object v)
            where T : class, new()
        {
            var rslt = new T();
            var destProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var typeHandler = v.GetType();
            foreach (var property in destProperties)
            {
                property.SetValue(rslt, typeHandler.GetProperty(property.Name).GetValue(v));
            }
            return rslt;
        }

        /// <summary>
        /// 从Entity集合中查询出对应的实体类型
        /// </summary>
        /// <typeparam name="F">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="storedEntities">源类型集合</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectTo<F, T>(this IQueryable<F> storedEntities)
            where T : class, new()
            where F : IStored
        {
            if (storedEntities == null || !storedEntities.Any())
            {
                return new List<T>(0);
            }
            var result = new List<T>();
            var rsltTemp = storedEntities.Select(GenerateSelectSQL<T>());
            foreach (var item in rsltTemp)
            {
                result.Add(ConvertTo<T>(item));
            }
            return result;
        }
    }
}
