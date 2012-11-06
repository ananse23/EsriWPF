using System;
using System.Diagnostics;
using log4net;

namespace Esri.CommonUtils
{
    public class Logger
    {

        public static void Log(string message)
        {
            var trace = new StackFrame(1);
            var methodBase = trace.GetMethod();
            var type = methodBase.DeclaringType;
            LogManager.GetLogger(type).Info(message);
        }

        public static void Error(string message, Exception ex)
        {
            LogManager.GetLogger("ErrorLogger").Fatal(message, ex);
        } 
    }
}