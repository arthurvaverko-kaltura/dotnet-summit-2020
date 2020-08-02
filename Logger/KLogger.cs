using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using System.Collections.Concurrent;
using log4net.Core;
using log4net.Repository;
using log4net.Util;
using log4net.Appender;
using System.Runtime.CompilerServices;
using System.Xml;
using System.IO;
using System.Web;

namespace KLogMonitor
{
    [Serializable]
    public class KLogger
    {
        // this logger is used to log Klogger configuration etc...
        private static readonly ILog _InternalLogger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public const string ACTION = "kmon_action";
        public const string REQUEST_ID_KEY = "kmon_req_id";
        public const string GROUP_ID = "kmon_group_id";
        public const string CLIENT_TAG = "kmon_client_tag";
        public const string METHOD_NAME = "kmon_method_name";
        public const string HOST_IP = "kmon_host_ip";
        public const string USER_ID = "kmon_user_id";
        public const string TOPIC = "kmon_topic";
        public const string TOPIC_LOG = "kmon_topic_log";
        public const string MULTIREQUEST = "kmon_multirequest";
        public const string CLASS_NAME = "kmon_class";
        public const string SERVER = "kmon_server";
        public const string LOGGER_NAME = "kmon_log_name";
        public const string IP_ADDRESS = "kmon_ip_address";



        private readonly ILog _Logger;
        private static ILoggerRepository _LogRepository;
        private static string configurationFileName;

        public KLogger(string className, string separateLoggerName = null) : this(separateLoggerName ?? className) { }

        public KLogger(string className)
        {
            var repo = GetLoggerRepository();
            _Logger = LogManager.GetLogger(repo.Name, className);
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[SERVER] = Environment.MachineName;
                HttpContext.Current.Items[CLASS_NAME] = className;
                HttpContext.Current.Items[LOGGER_NAME] = string.Empty;
                HttpContext.Current.Items[CLASS_NAME] = className;
            }
        }



        #region Initialization and configuration


        public static void Configure(string logConfigFile)
        {
            var repository = GetLoggerRepository();
            var file = new System.IO.FileInfo(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, logConfigFile));

            if (!repository.Configured)
            {
                log4net.Config.XmlConfigurator.Configure(repository, file);
            }

            if (SystemInfo.NullText == "(null)")
            {
                SystemInfo.NullText = "null";
            }

            configurationFileName = logConfigFile;
        }





        #endregion



        internal static ILoggerRepository GetLoggerRepository()
        {
            if (_LogRepository != null) return _LogRepository;

            var repo = LogManager.GetAllRepositories().FirstOrDefault();
            _LogRepository = repo;
            return repo;
        }

        #region Message Handling

        private void HandleEvent(string msg, KLogger.LogEvent.LogLevel level, object[] args, Exception ex = null, string callerMemberName = null)
        {
            try
            {
                if (args != null && ex != null)
                    throw new Exception("Args and Exception cannot co exist");

                var le = new LogEvent
                {
                    Message = FormatMessage(msg),
                    Exception = ex,
                    Level = level,
                    args = args
                };

                SendLog(le);
            }
            catch (Exception logException)
            {
                _Logger.ErrorFormat("Klogger Error in handle event. original log message: {0}, ex: {1}", msg, logException);
            }
        }

        private void SendLog(LogEvent logEvent)
        {
            if (logEvent.args != null && logEvent.args.Any())
            {
                switch (logEvent.Level)
                {
                    case LogEvent.LogLevel.INFO:
                        _Logger.InfoFormat(logEvent.Message, logEvent.args);
                        break;
                    case LogEvent.LogLevel.DEBUG:
                        _Logger.DebugFormat(logEvent.Message, logEvent.args);
                        break;
                    case LogEvent.LogLevel.WARNING:
                        _Logger.WarnFormat(logEvent.Message, logEvent.args);
                        break;
                    case LogEvent.LogLevel.ERROR:
                        _Logger.ErrorFormat(logEvent.Message, logEvent.args);
                        break;
                    default:
                        _Logger.DebugFormat(logEvent.Message, logEvent.args);
                        break;
                }
            }
            else
            {
                switch (logEvent.Level)
                {
                    case LogEvent.LogLevel.INFO:
                        _Logger.Info(logEvent.Message, logEvent.Exception);
                        break;
                    case LogEvent.LogLevel.DEBUG:
                        _Logger.Debug(logEvent.Message, logEvent.Exception);
                        break;
                    case LogEvent.LogLevel.WARNING:
                        _Logger.Warn(logEvent.Message, logEvent.Exception);
                        break;
                    case LogEvent.LogLevel.ERROR:
                        _Logger.Error(logEvent.Message, logEvent.Exception);
                        break;
                    default:
                        _Logger.Debug(logEvent.Message, logEvent.Exception);
                        break;
                }
            }
        }

        private string FormatMessage(string msg)
        {
            return string.Format("class:{0} topic:{1} method:{2} server:{3} ip:{4} reqid:{5} msg:{6}",
                HttpContext.Current?.Items[CLASS_NAME],      // 0
                HttpContext.Current?.Items[TOPIC],           // 1
                HttpContext.Current?.Items[METHOD_NAME],     // 2
                HttpContext.Current?.Items[SERVER],          // 3
                HttpContext.Current?.Items[IP_ADDRESS],      // 4
                HttpContext.Current?.Items[REQUEST_ID_KEY],  // 5
                msg);                                       // 6
        }

        #endregion

        #region Logging Methods

        public void Debug(string sMessage, Exception ex = null, [CallerMemberName] string callerMemberName = null)
        {
            HandleEvent(sMessage != null ? sMessage : string.Empty, KLogger.LogEvent.LogLevel.DEBUG, null, ex, callerMemberName);
        }

        public void DebugFormat(string format, params object[] args)
        {
            HandleEvent(format, KLogger.LogEvent.LogLevel.DEBUG, args, null);
        }

        public void Info(string sMessage, Exception ex = null, [CallerMemberName] string callerMemberName = null)
        {
            HandleEvent(sMessage != null ? sMessage : string.Empty, KLogger.LogEvent.LogLevel.INFO, null, ex, callerMemberName);
        }

        public void InfoFormat(string format, params object[] args)
        {
            HandleEvent(format, KLogger.LogEvent.LogLevel.INFO, args, null);
        }

        public void Warn(string sMessage, Exception ex = null, [CallerMemberName] string callerMemberName = null)
        {
            HandleEvent(sMessage != null ? sMessage : string.Empty, KLogger.LogEvent.LogLevel.WARNING, null, ex, callerMemberName);
        }

        public void WarnFormat(string format, params object[] args)
        {
            HandleEvent(format, KLogger.LogEvent.LogLevel.WARNING, args, null);
        }

        public void Error(string sMessage, Exception ex = null, [CallerMemberName] string callerMemberName = null)
        {
            HandleEvent(sMessage != null ? sMessage : string.Empty, KLogger.LogEvent.LogLevel.ERROR, null, ex, callerMemberName);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            HandleEvent(format, KLogger.LogEvent.LogLevel.ERROR, args, null);
        }

        #endregion

        private class LogEvent
        {
            public enum LogLevel { INFO, DEBUG, WARNING, ERROR }
            public string Message { get; set; }
            public Exception Exception { get; set; }
            public LogLevel Level { get; set; }
            public object[] args { get; set; }
        }
    }
}

