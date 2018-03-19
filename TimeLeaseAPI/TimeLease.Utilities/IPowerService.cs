
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rita.System.Power
{
    public interface IPowerService
    {
        /// <summary>
        /// 根据路由获取路由对应的权限要求
        /// </summary>
        /// <param name="controllName">控制器名称</param>
        /// <param name="actionName">行为名称</param>
        /// <returns>路由对应的权限要求</returns>
        int[] GetRouteRequestCodes(string controllName, string actionName);
        /// <summary>
        /// 获取用户对应的权限列表
        /// </summary>
        /// <param name="userID">用户名称</param>
        /// <returns>用户权限列表</returns>
        int[] GetUserAssignedEventCodes(string userID);
    }
}
