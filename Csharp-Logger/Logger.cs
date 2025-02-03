using System;
using System.IO;
using System.Reflection.Emit;

namespace Logger
{
    public class Logger
    {
        public void LogMessage(string filepath, string message, string entryType)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string logEntry = $"[{entryType}] {message}";
            using (var w = File.AppendText(filepath))
            {
                w.WriteLine($"[{timestamp}]" + logEntry);
            }
            Console.WriteLine($"[{timestamp}]" + logEntry);
        }
    }
}

