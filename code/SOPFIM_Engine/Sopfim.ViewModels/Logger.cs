using System;
using log4net;

namespace Sopfim.ViewModels
{
    public class Logger
    {
        public static void Log(Type type, string message)
        {
            LogManager.GetLogger(type).Info(message);
        }

        public static void Error(string message, Exception ex)
        {
            LogManager.GetLogger("ErrorLogger").Fatal(message, ex);
        }
    }
}