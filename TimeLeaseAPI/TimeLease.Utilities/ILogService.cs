using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rita.System
{
    public interface ILogService
    {
        void LogInfo(string info, string operation);

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        void LogException(string info, Exception se, string operation);
    }
}
