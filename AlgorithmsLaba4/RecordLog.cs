using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4
{
    internal class RecordLog
    {
        private Level level;
        private string name;
        private string message;
        private DateTime date;
        public RecordLog(Level level, string message)
        {
            this.level = level;
            this.message = message;
        }
        public void SetLevel(Level level)
        {
            this.level = level;
        }
        public void SetLoggerName(string name)
        {
            this.name = name;
        }
        public void SetMessage(string message)
        {
            this.message = message;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public Level GetLevel()
        {
            return level;
        }
        public string GetName()
        {
            if (name == null)
            {
                name = "No Name Logger";
            }
            return name;
        }
        public string GetMessage()
        {
            return message;
        }
        public DateTime GetDate()
        {
            return date;
        }
        public string GetLogMessage()
        {
            if (name == null)
            {
                name = "No Name Logger";
            }
            if (date == null)
            {
                date = DateTime.Now;
            }
            return "[" + level.ToString() + "] " + GetCurrentDate() + " " + name + " - " + message;
        }
        private string GetCurrentDate()
        {
            return date.ToString("dd.MM.yyyy-hh:mm:ss");
        }
    }
}
