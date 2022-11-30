using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4
{
    internal class FileHandler : IMessageHandler
    {
        private string pathName;
        private RecordLog recordLog;
        public FileHandler(string pathName)
        {
            this.pathName = pathName;
            string path = $"..\\..\\..\\..\\Log\\{pathName}.txt";
            StreamWriter sr = new StreamWriter(path, false);
            sr.WriteLine("");
            sr.Close();
            //File.WriteAllText(pathName, "");
        }
        public void Publish(RecordLog recordLog)
        {
            this.recordLog = recordLog;
            WriteLog();
        }
        private void WriteLog()
        {
            string path = $"..\\..\\..\\..\\Log\\{pathName}.txt";
            try
            {
                string text = recordLog.GetLogMessage();
                File.AppendAllText(path, text + "\n");
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
