using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLaba4
{
    internal interface IMessageHandler
    {
        public void Publish(RecordLog recordLog);
    }
}
