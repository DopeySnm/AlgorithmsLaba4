using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4
{
    internal class ConsoleHandler : IMessageHandler
    {
        public void Publish(RecordLog recordLog)
        {
            Console.WriteLine(recordLog.GetLogMessage()); 
        }
    }
}
