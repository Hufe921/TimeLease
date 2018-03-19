using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rita.Utilities.Helper
{
    public class LogHelper
    {
        private readonly static ILog log = LogManager.GetLogger("TimeLeaseLog");
        private const string ERROR_FORMAT = @"异常:\r\n时间：{0}\r\n异常内容：{1}\r\n堆栈信息{2}\r\n-----------------------------";
        private const string INFO_FORMAT = @"消息:\r\n时间:{0}\r\n消息内容:{1}\r\n-----------------------------";
        private const string WARN_FORMAT = @"警告:\r\n时间:{0}\r\n警告内容:{1}\r\n-----------------------------";

        /// <summary>
        /// 将异常信息写入日志文件
        /// </summary>
        /// <param name="exception">异常信息</param>
        public static void LogError(Exception exception)
        {
            log.ErrorFormat(ERROR_FORMAT, DateTime.Now, exception.Message, exception.StackTrace);
        }

        /// <summary>
        /// 将消息内容写入日志文件
        /// </summary>
        /// <param name="infoMessage">消息信息</param>
        public static void LogInfo(string infoMessage)
        {
            log.InfoFormat(INFO_FORMAT, DateTime.Now, infoMessage);
        }

        /// <summary>
        /// 将警告信息写入日志文件
        /// </summary>
        /// <param name="warnMessage">警告信息</param>
        public static void LogWarn(string warnMessage)
        {
            log.WarnFormat(WARN_FORMAT, DateTime.Now, warnMessage);
        }
    }
}
