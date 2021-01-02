using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrSlideshow_1._0
{
    static class Debug
    {
        private static string logFolderPath = $@"{Directory.GetCurrentDirectory()}\Logs";
        private static string logFilePath = $@"{logFolderPath}\log.txt";

        public static void CreateDebugFiles()
        {
            Directory.CreateDirectory(logFolderPath);
            var logFileStream = File.Create(logFilePath);
            logFileStream.Close();
        }

        public static void Log(string logMessage)
        {
            if (!File.Exists(logFilePath))
            {
                CreateDebugFiles();
            }
            File.AppendAllText(logFilePath, logMessage);
        }

        public static void OpenLogFile()
        {
            if (!File.Exists(logFilePath))
            {
                CreateDebugFiles();
            }

            Process.Start(logFilePath);
        }
    }
}