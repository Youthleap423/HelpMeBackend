using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace HelpMe.Helpers
{
    public class LogManager
    {
        private static bool IsConfigured;
        private static object ConfigurationLock = new object();

        /// <summary>
        /// Configure log4net using a specific configuration file.
        /// </summary>
        /// <param name="configFilePath">The path to the config file to use</param>
        public static void Configure(string configFilePath)
        {
            if (!IsConfigured)
            {
                lock (ConfigurationLock) //make sure we don't end up trying to configure twice at the same time
                {
                    if (!IsConfigured)
                    {
                        var configFile = new FileInfo(configFilePath);
                        XmlConfigurator.ConfigureAndWatch(configFile);
                        IsConfigured = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the logger for the calling class.
        /// </summary>
        /// <returns></returns>
        public static ILog GetLogger()
        {
            StackFrame frame = new StackTrace().GetFrame(1);
            return log4net.LogManager.GetLogger(frame.GetMethod().DeclaringType);
        }

        public static void Log(Exception ex)
        {
            ILog Logger = GetLogger();
            if (Logger.IsErrorEnabled) Logger.Error(ex.Message, ex);
        }
    }
}