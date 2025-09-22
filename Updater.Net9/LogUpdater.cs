using System;
using System.IO;
using System.Reflection;

namespace Logging
{

    public class LoggUpdater
    {
        private static readonly object _syncObject = new object();

        public enum LogLevel
        {
            ERROR_LOG = 0,
            INFO_LOG = 1,
            WARNING_LOG = 2,
        }
        public static LoggUpdater log = new LoggUpdater();

        private string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string datatime = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss");
     
        public LoggUpdater()
        {
            
        }

        public void LogWrite(LogLevel level ,string logMessage, params object[] args)
        {
            var logPath = m_exePath + "\\log\\" + datatime + "\\";
            lock (_syncObject)
            {
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                    }
                    using (StreamWriter w = File.AppendText(logPath +"updater.txt"))
                    {
                        Log(level, w, logMessage, args);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void Log(LogLevel level, TextWriter w, string logMessage, params object[] args)
        {
            string lvl = "INFO";
            switch(level)
            {
                case LogLevel.ERROR_LOG:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    lvl = "ERROR";
                    break;
                case LogLevel.INFO_LOG:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    lvl = "INFO";
                    break;
                case LogLevel.WARNING_LOG:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    lvl = "WARNING";
                    break;
            }

            w.WriteLine("{0}\t {1} {2}", DateTime.Now.ToString("yyyy-MM-ddhh:mm:ss.fff tt"), lvl, String.Format(logMessage, args));
            

        }
    }
}