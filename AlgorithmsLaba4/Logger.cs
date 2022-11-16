using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4
{
    internal class Logger
    {
        private string name;
        private Level level = Level.DEBUG;
        private List<IMessageHandler> messageHandlers;
        public Logger(string name)
        {
            this.name = name;
            messageHandlers = new List<IMessageHandler>();
        }
        public string GetName()
        {
            return name;
        }
        public void SetLevel(Level level)
        {
            this.level = level;
        }
        public Level GetLevel()
        {
            return level;
        }
        public string Log(Level level, string message)
        {
            if (this.level >= level)
            {
                RecordLog recordLog = new RecordLog(level, message);
                recordLog.SetLoggerName(name);
                recordLog.SetDate(DateTime.Now);
                if (messageHandlers.Count != 0)
                {
                    foreach (var item in messageHandlers)
                    {
                        item.Publish(recordLog);
                    }
                }
                return recordLog.GetLogMessage();
            }
            return "";
        }
        public void addMessageHandler(IMessageHandler messageHandler)
        {
            messageHandlers.Add(messageHandler);
        }
        public void removeMessageHandler(IMessageHandler messageHandler)
        {
            messageHandlers.Remove(messageHandler);
        }
    }
    public enum Level
    {
        DEBUG = 1,
        INFO,
        WARN,
        ERROR
    }
}
