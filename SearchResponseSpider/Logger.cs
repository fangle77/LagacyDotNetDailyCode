using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SearchResponseSpider
{
    class Logger : IDisposable
    {
        private const int BufferSize = 512;

        private StringBuilder buffer = new StringBuilder(BufferSize);

        private readonly string loggerFile;

        public Logger(string name)
        {
            loggerFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (Directory.Exists(loggerFile) == false)
            {
                Directory.CreateDirectory(loggerFile);
            }
            loggerFile = Path.Combine(loggerFile, name + ".log");
        }

        private static object LockKey = new object();

        public void Log(string content)
        {
            lock (LockKey)
            {
                buffer.AppendLine(content);
                if (buffer.Length >= BufferSize)
                {
                    FlushToFile();
                }
            }
        }

        public void FlushToFile()
        {
            try
            {
                File.AppendAllText(loggerFile, buffer.ToString());
            }
            catch
            {
            }
            buffer.Clear();
        }

        public void Dispose()
        {
            FlushToFile();
        }
    }
}
