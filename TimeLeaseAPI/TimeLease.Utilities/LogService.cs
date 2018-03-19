using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;

namespace Rita.System
{
    public class LogService : ILogService
    {
        //log4net日志专用
        public readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("TimeLease.log.info");
        public readonly log4net.ILog logerror = log4net.LogManager.GetLogger("TimeLease.log.error");

        public void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }

        /// <summary>
        /// 普通的文件记录日志
        /// </summary>
        /// <param name="info"></param>
        public void LogInfo(string info, string operation)
        {
            if (loginfo.IsInfoEnabled)
            {
                var builder = new StringBuilder(string.Format("Info Operation:{0}", operation));
                builder.AppendLine();
                builder.Append(info);
                loginfo.Info(builder.ToString());

            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="virtualinfo"></param>
        /// <param name="se"></param>
        public virtual void LogException(string info, Exception se, string operation)
        {
            if (logerror.IsErrorEnabled)
            {
                var builder = new StringBuilder(string.Format("Info Operation:{0}", operation));
                builder.AppendLine();
                builder.Append(info);
                logerror.Error(builder.ToString(), se);
            }
        }
    }
}
