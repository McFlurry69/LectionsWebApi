using System;
using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Config;
using log4net.Core;

namespace ConnectLibrary.Logger
{
    public class Log4NetLogger : ILogging
    {
        public Log4NetLogger()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            
        }

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public void MakeLog(LoggerOperations op, string message)
        {
            if (message == null)
            {
                message = "Empty message";
            }
            
            //var logger = LogManager.GetCurrentClassLogger();
            switch (op)
            {
                case LoggerOperations.Debug:
                    Logger.Debug(message);
                    break;
                case LoggerOperations.Info:
                    Logger.Info(message);
                    break;
                case LoggerOperations.Warn:
                    Logger.Warn(message);
                    break;
                case LoggerOperations.Error:
                    Logger.Error(message);
                    break;
            }
        }
    }
}