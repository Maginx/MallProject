using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallHost.Log
{
    sealed class Logging
    {
        /// <summary>
        /// Log error info instance
        /// </summary>
        private static Logger loggerInfo = LogManager.GetLogger("info");

        /// <summary>
        /// Log error message instance
        /// </summary>
        private static Logger loggerError = LogManager.GetLogger("error");

        private Logging()
        {

        }

        /// <summary>
        /// Log info message
        /// </summary>
        /// <param name="message">message content,string</param>
        public static void InfoMessage(string message)
        {
            loggerInfo.Info(message);
        }

        /// <summary>
        /// Log error message to errorlogs
        /// </summary>
        /// <param name="e">Excetpion instance for trace stack information</param>
        public static void ErrorMessage(Exception e)
        {
            loggerError.Error(e);
        }

        /// <summary>
        /// Log error message to errorlogs
        /// </summary>
        /// <param name="message">message content,string</param>
        /// <param name="e">exception instance for trace stack</param>
        public static void ErrorMessage(string message, Exception e)
        {
            loggerError.Error(e, message);
        }

        /// <summary>
        /// Debug message only for debug
        /// </summary>
        /// <param name="message">message content,string</param>
        public static void Debug(string message)
        {
            loggerInfo.Debug(message);
        }
    }
}
